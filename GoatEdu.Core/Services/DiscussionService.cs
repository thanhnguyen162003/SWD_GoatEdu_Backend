using System.Net;
using AutoMapper;
using FluentValidation;
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
    private readonly IValidator<DiscussionDto> _validator;


    public DiscussionService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime,
        IClaimsService claimsService, IOptions<PaginationOptions> options, ICloudinaryService cloudinaryService,
        IValidator<DiscussionDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentTime = currentTime;
        _claimsService = claimsService;
        _paginationOptions = options.Value;
        _cloudinaryService = cloudinaryService;
        _validator = validator;
    }
    
    public async Task<ResponseDto> CreateDiscussion(DiscussionDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var tags = await CheckAndAddTags(dto.Tags);
        if (!tags.Any()) return new ResponseDto(HttpStatusCode.NotFound, "Không đủ 4 tags");

        var mapper = _mapper.Map<Discussion>(dto);
        
        if (dto.DiscussionImageConvert != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.DiscussionImageConvert);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }

            mapper.DiscussionImage = image.Url.ToString();
        }
        
        mapper.Tags = tags;
        mapper.IsSolved = false;
        mapper.DiscussionVote = 0;
        mapper.Status = StatusConstraint.UNAPPROVED;
        mapper.UserId = _claimsService.GetCurrentUserId;
        mapper.CreatedBy = _claimsService.GetCurrentFullname;
        mapper.CreatedAt = _currentTime.GetCurrentTime();
        mapper.IsDeleted = false;

        await _unitOfWork.DiscussionRepository.AddAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Add Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Add Failed!");
    }
    
    public async Task<ResponseDto> UpdateDiscussion(Guid guid, DiscussionDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var userId = _claimsService.GetCurrentUserId;
        var disscussion = await _unitOfWork.DiscussionRepository.GetDiscussionByIdAndUserId(guid, userId);
        if (disscussion is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có quyền cập nhật");
        }

        if (dto.Tags.Count > 0)
        {
            var tag = await CheckAndAddTags(dto.Tags);
            if (!tag.Any())
            {
                return new ResponseDto(HttpStatusCode.NotFound, "Có lỗi lúc add tag mới rồi!");
            }

            disscussion.Tags.Clear();
            
            await _unitOfWork.SaveChangesAsync();
            
            disscussion.Tags = tag;
        }
        
        if (dto.DiscussionImageConvert != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.DiscussionImageConvert);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }
            disscussion.DiscussionImage = image.Url.ToString();
        }
        
        disscussion.DiscussionName = dto.DiscussionName ?? disscussion.DiscussionName;
        disscussion.DiscussionBody = dto.DiscussionBody ?? disscussion.DiscussionBody;
        disscussion.DiscussionBodyHtml = dto.DiscussionBodyHtml ?? disscussion.DiscussionBodyHtml;
        disscussion.IsSolved = dto.IsSolved ?? disscussion.IsSolved;
        disscussion.UpdatedBy = _claimsService.GetCurrentFullname;
        disscussion.UpdatedAt = _currentTime.GetCurrentTime();
        disscussion.Status = StatusConstraint.UNAPPROVED;

        _unitOfWork.DiscussionRepository.Update(disscussion);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }

    public async Task<PagedList<DiscussionDto>> GetTopDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var discussions = await _unitOfWork.DiscussionRepository.GetSignificantDiscussionByFilter(queryFilter);

        if (!discussions.Any()) return new PagedList<DiscussionDto>(new List<DiscussionDto>(), 0, 0, 0);

        var mapper = _mapper.Map<IEnumerable<DiscussionDto>>(discussions);
        
        return PagedList<DiscussionDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> DeleteDiscussions(List<Guid> guids)
    {
        var userId = _claimsService.GetCurrentUserId;
        await _unitOfWork.DiscussionRepository.SoftDelete(guids, userId);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
    }

    public async Task<PagedList<DiscussionDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var discussions = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(null, queryFilter);

        if (!discussions.Any()) return new PagedList<DiscussionDto>(new List<DiscussionDto>(), 0, 0, 0);
        
        var userId = _claimsService.GetCurrentUserId;
        var discussionIdVote = new List<Guid?>();
        
        if (userId != Guid.Empty)
        {
            var discussionIds = discussions.Select(x => x.Id);
            discussionIdVote = await _unitOfWork.VoteRepository.GetDiscussionVoteByUserId(userId, discussionIds);
        }

        var mapper = discussions.Select(discussion =>
        {
            var dto = _mapper.Map<DiscussionDto>(discussion);
            dto.IsUserVoted = userId != Guid.Empty && discussionIdVote.Contains(dto.Id);
            return dto;
        });

        return PagedList<DiscussionDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<DiscussionDto?> GetDiscussionById(Guid guid)
    {
        var result = await _unitOfWork.DiscussionRepository.GetById(guid);

        if (result == null) return null;

        var userId = _claimsService.GetCurrentUserId;
        var discussionIdVote = new List<Guid?>();
        
        if (userId != Guid.Empty)
        {
            var discussionIds = new List<Guid>
            {
                result.Id
            };
            discussionIdVote = await _unitOfWork.VoteRepository.GetDiscussionVoteByUserId(userId, discussionIds);
        }
        var mapper = _mapper.Map<DiscussionDto>(result);
        mapper.IsUserVoted = userId != Guid.Empty && discussionIdVote.Contains(result.Id);
        return mapper;
    }

    public async Task<PagedList<DiscussionDto>> GetDiscussionByUserId(DiscussionQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var userId = _claimsService.GetCurrentUserId;
        var list = await _unitOfWork.DiscussionRepository.GetDiscussionByFilters(userId, queryFilter);

        if (!list.Any()) return new PagedList<DiscussionDto>(new List<DiscussionDto>(), 0, 0, 0);

        var mapper = _mapper.Map<List<DiscussionDto>>(list);
        return PagedList<DiscussionDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }

    private async Task<List<Tag>> CheckAndAddTags(IEnumerable<TagDto> tagDtos)
    {
        var tagNames = tagDtos.Select(x => x.TagName.ToLower()).ToList();
        var tagCheck = await _unitOfWork.TagRepository.CheckTagByNamesAsync(tagNames);
        var tagNameNoExist = tagNames.ExceptBy(tagCheck.Select(x => x.TagName).ToList(), x => x);

        if (tagNameNoExist.Any())
        {
            var newTags = tagNameNoExist.Select(x =>
            {
                var tag = new Tag
                {
                    TagName = x,
                    CreatedAt = _currentTime.GetCurrentTime(),
                    IsDeleted = false
                };
                return tag;
            }).ToList();
            await _unitOfWork.TagRepository.AddRangeAsync(newTags);
            var save = await _unitOfWork.SaveChangesAsync();
            if (save < 1) return new List<Tag>();  
        }
        
        return await _unitOfWork.TagRepository.GetTagByNames(tagNames);
    }
}