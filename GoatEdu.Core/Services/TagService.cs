using System.Net;
using AutoMapper;
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
    private readonly IClaimsService _claimsService;
    private readonly PaginationOptions _paginationOptions;
    
    public TagService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, IClaimsService claimsService, IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentTime = currentTime;
        _claimsService = claimsService;
        _paginationOptions = options.Value;
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

    public async Task<ResponseDto> getTagByName(List<string> name)
    {
        var tagFound = await _unitOfWork.TagRepository.GetTagNameByNameAsync(name);
        
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

    public async Task<ResponseDto> InsertTags(List<TagDto> tagRequestDtos)
    {
        var listName = tagRequestDtos.Select(x => x.TagName).ToList();
        
        var listExistName = await _unitOfWork.TagRepository.GetTagNameByNameAsync(listName);

        var tagIsDuplicated = new List<TagDto>();
        
        // Check Dup Name
        if (listExistName.Any())
        {
            tagIsDuplicated = tagRequestDtos.Join(
                listExistName.Select(a => a.TagName).ToList(), 
                x => x.TagName,
                name => name,
                (x, _) => x).ToList();
                
            tagRequestDtos = tagRequestDtos.Where(x =>
                    !listExistName.Any(name => name.Equals(x.TagName)))
                .ToList();
        }

        var tagMapper = tagRequestDtos.Select(x =>
            {
                var tag = _mapper.Map<Tag>(x);
                tag.CreatedAt = _currentTime.GetCurrentTime();
                tag.IsDeleted = false;
                return tag;
            }
        ).ToList();
        
        await _unitOfWork.TagRepository.AddRangeAsync(tagMapper);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Add Successfully!", tagIsDuplicated);
        }
        return new ResponseDto(HttpStatusCode.BadRequest, "Add Failed!", tagIsDuplicated);
    }

    public async Task<ResponseDto> DeleteTags(List<Guid> guids)
    {
        await _unitOfWork.TagRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result < 1)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
        }
        
        var flashFound = await _unitOfWork.FlashcardRepository.GetTwoTagFlashcard();
        foreach (var data in flashFound)
        {
            data.Status = "Disable";
        }
        
        _unitOfWork.FlashcardRepository.UpdateRange(flashFound);
        var result1 = await _unitOfWork.SaveChangesAsync();
        
        if (result1 < 1)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Something went wrong at Update Falshcard!");
        }

        return new ResponseDto(HttpStatusCode.OK, "Delete Successfully");
    }

    public async Task<ResponseDto> UpdateTag(Guid id, TagDto tagRequestDto)
    {
        var tagFound = await _unitOfWork.TagRepository.GetByIdAsync(id);
        if (tagFound == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Kiếm có thấy đâu");
        }

        tagFound = _mapper.Map(tagRequestDto, tagFound);
        tagFound.UpdatedAt = _currentTime.GetCurrentTime();
        
        _unitOfWork.TagRepository.Update(tagFound);
        var result = await _unitOfWork.SaveChangesAsync();

        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Update Successfully!");
        }
        return new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}