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

    public async Task<PagedList<DiscussionDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        queryFilter.page_number =
            queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
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
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var tag = await CheckAndAddTags(dto.Tags);
        if (!tag.Any()) return new ResponseDto(HttpStatusCode.NotFound, "Có lỗi lúc add tag mới rồi!");

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

        mapper.Tags = (ICollection<Tag>)tag;
        mapper.IsSolved = false;
        mapper.DiscussionVote = 0;
        mapper.Status = DiscussionStatus.Unapproved.ToString();
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

    public async Task<ResponseDto> DeleteDiscussions(List<Guid> guids)
    {
        var userId = _claimsService.GetCurrentUserId;
        await _unitOfWork.DiscussionRepository.SoftDelete(guids, userId);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
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
        var disscussion = await _unitOfWork.DiscussionRepository.GetByIdAndUserId(guid, userId);
        if (disscussion is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Không có quyền cập nhật");
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

        if (dto.Tags != null)
        {
            var tag = await CheckAndAddTags(dto.Tags);
            if (!tag.Any())
            {
                return new ResponseDto(HttpStatusCode.NotFound, "Có lỗi lúc add tag mới rồi!");
            }

            disscussion.Tags = (ICollection<Tag>)tag;
        }

        disscussion.DiscussionName = dto.DiscussionName ?? disscussion.DiscussionName;
        disscussion.DiscussionBody = dto.DiscussionBody ?? disscussion.DiscussionBody;
        disscussion.IsSolved = dto.IsSolved ?? disscussion.IsSolved;
        disscussion.UpdatedBy = _claimsService.GetCurrentFullname;
        disscussion.UpdatedAt = _currentTime.GetCurrentTime();
        disscussion.Status = DiscussionStatus.Unapproved.ToString();

        _unitOfWork.DiscussionRepository.Update(disscussion);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }

    private async Task<IEnumerable<Tag?>> CheckAndAddTags(IEnumerable<TagDto> tagDtos)
    {
        var tagNames = tagDtos.Select(x => x.TagName.ToLower()).ToList();
        var tagCheck = await _unitOfWork.TagRepository.GetTagByNamesAsync(tagNames);
        var tagNameNoExist = tagNames.ExceptBy(tagCheck.Select(x => x.TagName).ToList(), x => x);

        if (tagNameNoExist.Any())
        {
            var tags = tagNameNoExist.Select(x =>
            {
                var tag = new Tag
                {
                    TagName = x,
                    CreatedAt = _currentTime.GetCurrentTime(),
                    IsDeleted = false
                };
                return tag;
            }).ToList();
            await _unitOfWork.TagRepository.AddRangeAsync(tags);
            var save = await _unitOfWork.SaveChangesAsync();
            if (save < 1) return Enumerable.Empty<Tag>();
        }
        
        var tag = await _unitOfWork.TagRepository.GetTagByNamesAsync(tagNames);
        return tag.Count() == 4 ? tag : Enumerable.Empty<Tag>();
    }
}