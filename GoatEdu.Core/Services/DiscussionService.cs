using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
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
    private readonly ICloudinaryService _cloudinaryService;


    public DiscussionService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime,
        IClaimsService claimsService, IOptions<PaginationOptions> options, ICloudinaryService cloudinaryService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentTime = currentTime;
        _claimsService = claimsService;
        _paginationOptions = options.Value;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<PagedList<DiscussionResponseDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(null, queryFilter);
       
        if (!list.Any()) return new PagedList<DiscussionResponseDto>(new List<DiscussionResponseDto>(), 0, 0, 0);
        
        var mapper = _mapper.Map<List<DiscussionResponseDto>>(list);
        return PagedList<DiscussionResponseDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> GetDiscussionById(Guid guid)
    {
        var result = await _unitOfWork.DiscussionRepository.GetById(guid);
        
        if (result == null) return new ResponseDto(HttpStatusCode.NotFound, "Kiếm thấy đâu");
        
        var mapper = _mapper.Map<DiscussionDetailResponseDto>(result);
        return new ResponseDto(HttpStatusCode.OK, "", mapper);
    }

    public async Task<PagedList<DiscussionResponseDto>> GetDiscussionByUserId(DiscussionQueryFilter queryFilter)
    {
        var userId = _claimsService.GetCurrentUserId;
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(userId, queryFilter);
        
        if (!list.Any()) return new PagedList<DiscussionResponseDto>(new List<DiscussionResponseDto>(), 0, 0, 0);
        
        var mapper = _mapper.Map<List<DiscussionResponseDto>>(list);
        return PagedList<DiscussionResponseDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }


    public async Task<ResponseDto> InsertDiscussion(DiscussionRequestDto dto)
    {
        var tagNoExist = dto.Tags.Where(x => x.id == null).ToList();
        
        if (tagNoExist.Any())
        {
            var tags = tagNoExist.Select(x =>
            {
                var tag = new Tag
                {
                    TagName = x.TagName,
                    CreatedAt = _currentTime.GetCurrentTime(),
                    IsDeleted = false
                };
                return tag;
            }).ToList();
        
            await _unitOfWork.TagRepository.AddRangeAsync(tags);
            
            var save = await _unitOfWork.SaveChangesAsync();
            
            if (save < 1)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, "Có lỗi ở chỗ insertDiscussion");
            }
        }

        var names = dto.Tags.Select(x => x.TagName).ToList();
        var tag = await _unitOfWork.TagRepository.GetTagNameByNameAsync(names);
        var mapper = _mapper.Map<Discussion>(dto);

        if (dto.DiscussionImage != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.DiscussionImage);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }
        
            mapper.DiscussionImage = image.Url.ToString();
        }
        
        mapper.Tags = tag.ToList();
        mapper.IsSolved = false;
        mapper.DiscussionVote = 0;
        mapper.Status = DiscussionStatus.Unapproved.ToString();
        mapper.UserId = _claimsService.GetCurrentUserId;
        mapper.CreatedBy = _claimsService.GetCurrentFullname;
        mapper.CreatedAt = _currentTime.GetCurrentTime();
        mapper.IsDeleted = false;
        
        await _unitOfWork.DiscussionRepository.AddAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Add Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Add Failed!");
    }

    public async Task<ResponseDto> DeleteDiscussions(List<Guid> guids)
    {
        await _unitOfWork.DiscussionRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Delete Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
    }

    public async Task<ResponseDto> UpdateDiscussion(Guid guid, DiscussionRequestDto discussionRequestDto)
    {
        var disscussion = await _unitOfWork.DiscussionRepository.GetByIdAsync(guid);
        if (disscussion == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có");
        }

        if (disscussion.Status.Equals(DiscussionStatus.Approved))
        {
            return new ResponseDto(HttpStatusCode.Forbidden, "Update? Too Late :))");
        }
        
        disscussion = _mapper.Map(discussionRequestDto, disscussion);
        disscussion.UpdatedBy = _claimsService.GetCurrentFullname;
        disscussion.UpdatedAt = _currentTime.GetCurrentTime();
        _unitOfWork.DiscussionRepository.Update(disscussion);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Update Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}