using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.DiscussionInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class DiscussionService : IDiscussionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentTime _currentTime;
    private readonly IClaimsService _claimsService;
    private readonly PaginationOptions _paginationOptions;

    public DiscussionService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, IClaimsService claimsService, IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentTime = currentTime;
        _claimsService = claimsService;
        _paginationOptions = options.Value;
    }
    
    public async Task<PagedList<DiscussionResponseDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(null, queryFilter);
        if (!list.Any())
        {
            return new PagedList<DiscussionResponseDto>(new List<DiscussionResponseDto>(), 0, 0, 0);}

        var mapper = _mapper.Map<List<DiscussionResponseDto>>(list);
        return PagedList<DiscussionResponseDto>.Create(mapper, queryFilter.PageNumber, queryFilter.PageSize);
    }

    public async Task<ResponseDto> GetDiscussionById(Guid guid)
    {
        var result = await _unitOfWork.DiscussionRepository.GetById(guid);
        if (result == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Kiếm thấy đâu");
        }

        var mapper = _mapper.Map<DiscussionDetailResponseDto>(result);
        return new ResponseDto(HttpStatusCode.NotFound, "", mapper);
    }

    public async Task<PagedList<DiscussionResponseDto>> GetDiscussionByUserId(DiscussionQueryFilter queryFilter)
    {
        var userId = _claimsService.GetCurrentUserId;
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(userId, queryFilter);
        if (!list.Any())
        {
            return new PagedList<DiscussionResponseDto>(new List<DiscussionResponseDto>(), 0, 0, 0);}

        var mapper = _mapper.Map<List<DiscussionResponseDto>>(list);
        return PagedList<DiscussionResponseDto>.Create(mapper, queryFilter.PageNumber, queryFilter.PageSize); 
    }


    public async Task<ResponseDto> InsertDiscussion(DiscussionRequestDto discussionRequestDto)
    {
        var tagCheck = await _unitOfWork.TagRepository.GetTagNameByNameAsync(discussionRequestDto.Tags);
        var tagNoExist = discussionRequestDto.Tags.Except(tagCheck).ToList();
        // if (tagNoExist.Any())
        // {
        //     await _unitOfWork.TagRepository.AddRangeAsync();
        // }
        
        var mapper = _mapper.Map<Discussion>(discussionRequestDto);
        mapper.Status = DiscussionStatus.Unapproved.ToString();
        mapper.UserId = _claimsService.GetCurrentUserId;
        mapper.CreatedBy = _claimsService.GetCurrentUserId.ToString();
        await _unitOfWork.DiscussionRepository.AddAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Add Successfully!");
        }
        return new ResponseDto(HttpStatusCode.BadRequest, "Add Failed!");
    }

    public async Task<ResponseDto> DeleteDiscussions(List<Guid> guids)
    {
        await _unitOfWork.DiscussionRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Delete Successfully!");
        }
        return new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
    }

    public async Task<ResponseDto> UpdateDiscussion(Guid guid, DiscussionRequestDto discussionRequestDto)
    {
        var disscussion = await _unitOfWork.DiscussionRepository.GetByIdAsync(guid);
        if (disscussion == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có");
        }

        disscussion = _mapper.Map(discussionRequestDto, disscussion);
        disscussion.UpdatedBy = _claimsService.GetCurrentUserId.ToString();
        disscussion.UpdatedAt = _currentTime.GetCurrentTime();
        _unitOfWork.DiscussionRepository.Update(disscussion);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Update Successfully!");
        }

        return new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}