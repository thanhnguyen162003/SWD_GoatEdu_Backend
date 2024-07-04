using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.TagInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class TagService : ITagService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentTime _currentTime;
    private readonly PaginationOptions _paginationOptions;
    private readonly IValidator<TagDto> _validator;
    
    public TagService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, IOptions<PaginationOptions> paginationOptions, IValidator<TagDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentTime = currentTime;
        _paginationOptions = paginationOptions.Value;
        _validator = validator;
    }

    public async Task<PagedList<TagDto>> GetTagByFilter(TagQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var listTag = await _unitOfWork.TagRepository.GetTagByFilters(queryFilter);
        
        if (!listTag.Any())
        {
            return new PagedList<TagDto>(new List<TagDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<TagDto>>(listTag);
        
        return PagedList<TagDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> GetTagByName(string name)
    {
        var tagFound = await _unitOfWork.TagRepository.GetTagByNameAsync(name);
        
        if (!tagFound.Any())
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Có đâu mà tìm");
        }

        return new ResponseDto(HttpStatusCode.OK, "Có rồi nè!", tagFound);
    }

    public async Task<ResponseDto> GetTagById(Guid guid)
    {
        var tagFound = await _unitOfWork.TagRepository.GetByIdAsync(guid);
        if (tagFound == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Kiếm không thấy :))");
        }
        var mapperNote = _mapper.Map<TagDto>(tagFound);
        return new ResponseDto(HttpStatusCode.OK, "", mapperNote);
    }

    public async Task<ResponseDto> InsertTags(List<TagDto> dtos)
    {
        var errors = new List<object>();
        foreach (var data in dtos)
        {
            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                var errorDetails = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage, data.TagName });
                errors.AddRange(errorDetails);
            }
        }

        if (errors.Any())
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }
        
        var listName = dtos.Select(x => x.TagName.ToLower()).ToList();
        
        var listExistName = await _unitOfWork.TagRepository.CheckTagByNamesAsync(listName);

        var tagIsDuplicated = new List<TagDto>();
        
        // Check Dup Name
        if (listExistName.Any())
        {
            tagIsDuplicated = dtos.Join(
                listExistName.Select(a => a.TagName).ToList(), 
                x => x.TagName,
                name => name,
                (x, _) => x).ToList();
                
            dtos = dtos.Where(x =>
                    !listExistName.Any(name => name.Equals(x.TagName)))
                .ToList();
        }

        var tagMapper = dtos.Select(x =>
            {
                var tag = new Tag
                {
                    TagName = x.TagName.ToLower(),
                    CreatedAt = _currentTime.GetCurrentTime(),
                    IsDeleted = false
                };
                return tag;
            }
        ).ToList();
        
        await _unitOfWork.TagRepository.AddRangeAsync(tagMapper);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Add Successfully!", tagIsDuplicated) : new ResponseDto(HttpStatusCode.BadRequest, "Add Failed!", tagIsDuplicated);
    }

    public async Task<ResponseDto> DeleteTags(List<Guid> guids)
    {
        await _unitOfWork.TagRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result < 1) 
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
        }
        
        var result1 = await _unitOfWork.SaveChangesAsync();
        
        return result1 < 1 ? new ResponseDto(HttpStatusCode.BadRequest, "Something went wrong at Update Falshcard!") : new ResponseDto(HttpStatusCode.OK, "Delete Successfully");
    }

    public async Task<ResponseDto> UpdateTag(Guid id, TagDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage, dto.TagName });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }
        
        var tagFound = await _unitOfWork.TagRepository.GetByIdAsync(id);
        if (tagFound == null) return new ResponseDto(HttpStatusCode.NotFound, "Kiếm có thấy đâu");

        tagFound.TagName = dto.TagName ?? tagFound.TagName;
        tagFound.UpdatedAt = _currentTime.GetCurrentTime();
        
        _unitOfWork.TagRepository.Update(tagFound);
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Update Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}