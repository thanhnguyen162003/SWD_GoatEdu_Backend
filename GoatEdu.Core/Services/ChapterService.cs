using System.Net;
using AutoMapper;
using CloudinaryDotNet.Actions;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class ChapterService : IChapterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly IValidator<ChapterDto> _validator;

    public ChapterService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> paginationOptions, IValidator<ChapterDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
        _validator = validator;
    }
    public async Task<ICollection<ChapterDto>> GetChapters(ChapterQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listChapter = await _unitOfWork.ChapterRepository.GetChapters(queryFilter);
        
        if (!listChapter.Any())
        {
            return new PagedList<ChapterDto>(new List<ChapterDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<ChapterDto>>(listChapter);
        return PagedList<ChapterDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }
    
    public async Task<ResponseDto> DeleteChapter(Guid id)
    {
        return await _unitOfWork.ChapterRepository.DeleteChapter(id);
    }

    public async Task<ResponseDto> UpdateChapter(ChapterDto dto, Guid chapterId)
    {
        return await _unitOfWork.ChapterRepository.UpdateChapter(dto, chapterId);
    }

    public async Task<ResponseDto> CreateChapter(ChapterDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }
        
        var newChapter = new Chapter
        {
            ChapterName = dto.ChapterName,
            ChapterLevel = dto.ChapterLevel,
            SubjectId = dto.SubjectId,
            CreatedBy = "fixing createdBy",
            CreatedAt = DateTime.Now,
            IsDeleted = false
        };

        await _unitOfWork.ChapterRepository.CreateChapter(newChapter);

        return new ResponseDto(HttpStatusCode.Created, "Chapter created successfully.", newChapter.Id);
    }

    public async Task<ChapterDto> GetChapterByChapterName(string chapterName)
    {
        return await _unitOfWork.ChapterRepository.GetChapterByChapterName(chapterName);
    }

    public async Task<ChapterDto> GetChapterByChapterId(Guid id)
    {
        return await _unitOfWork.ChapterRepository.GetChapterByChapterId(id);
    }
}