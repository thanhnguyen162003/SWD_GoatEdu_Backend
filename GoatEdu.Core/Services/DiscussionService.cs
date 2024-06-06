using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
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

    public async Task<PagedList<DiscussionDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(null, queryFilter);
       
        if (!list.Any()) return new PagedList<DiscussionDto>(new List<DiscussionDto>(), 0, 0, 0);
        
        var mapper = _mapper.Map<List<DiscussionDto>>(list);
        return PagedList<DiscussionDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> GetDiscussionById(Guid guid)
    {
        var result = await _unitOfWork.DiscussionRepository.GetById(guid);
        
        if (result == null) return new ResponseDto(HttpStatusCode.NotFound, "Kiếm thấy đâu");
        
        var mapper = _mapper.Map<DiscussionDto>(result);
        return new ResponseDto(HttpStatusCode.OK, "", mapper);
    }

    public async Task<PagedList<DiscussionDto>> GetDiscussionByUserId(DiscussionQueryFilter queryFilter)
    {
        var userId = _claimsService.GetCurrentUserId;
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(userId, queryFilter);
        
        if (!list.Any()) return new PagedList<DiscussionDto>(new List<DiscussionDto>(), 0, 0, 0);
        
        var mapper = _mapper.Map<List<DiscussionDto>>(list);
        return PagedList<DiscussionDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }


    public async Task<ResponseDto> InsertDiscussion(DiscussionDto dto)
    {
        var tag = await CheckAndAddTags(dto);
        
        if (!tag.Any())
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có");
        }
        var mapper = _mapper.Map<Discussion>(dto);

        if (dto.DiscussionImage != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.DiscussionImageConvert);
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

    public async Task<ResponseDto> UpdateDiscussion(Guid guid, DiscussionDto discussionRequestDto)
    {

        var tag = await CheckAndAddTags(discussionRequestDto);
        if (!tag.Any())
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có");
        }
        
        var disscussion = await _unitOfWork.DiscussionRepository.GetByIdAsync(guid);
        if (disscussion is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có");
        }
        
        disscussion = _mapper.Map(discussionRequestDto, disscussion);
        disscussion.Status = DiscussionStatus.Unapproved.ToString();
        disscussion.UpdatedBy = _claimsService.GetCurrentFullname;
        disscussion.UpdatedAt = _currentTime.GetCurrentTime();
        _unitOfWork.DiscussionRepository.Update(disscussion);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Update Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }

    private async Task<IEnumerable<Tag?>> CheckAndAddTags(DiscussionDto dto)
    {
            var tagNoExist = dto.Tags.Where(x => x.Id == null).ToList();
        
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
                if (save < 1) return Enumerable.Empty<Tag>();
            }

            var names = dto.Tags.Select(x => x.TagName);
            var tag = await _unitOfWork.TagRepository.GetTagNameByNameAsync(names);
            return tag.Count() == 4 ? tag : Enumerable.Empty<Tag>();
    } 
}