using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.Models;
using GoatEdu.Core.QueriesFilter;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class ChapterService : IChapterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;

    public ChapterService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> paginationOptions)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
    }

    public async Task<ICollection<ChapterResponseDto>> GetChapters(ChapterQueryFilter queryFilter)
    {
        queryFilter.PageNumber = queryFilter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.PageNumber;
        queryFilter.PageSize = queryFilter.PageSize == 0 ? _paginationOptions.DefaultPageSize : queryFilter.PageSize;
        
        var listChapter = await _unitOfWork.ChapterRepository.GetChapters(queryFilter);
        
        if (!listChapter.Any())
        {
            return new PagedList<ChapterResponseDto>(new List<ChapterResponseDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<ChapterResponseDto>>(listChapter);
        return PagedList<ChapterResponseDto>.Create(mapperList, queryFilter.PageNumber, queryFilter.PageSize);
    }
    

    public async Task<ResponseDto> DeleteChapter(Guid id)
    {
        return await _unitOfWork.ChapterRepository.DeleteChapter(id);
    }

    public async Task<ResponseDto> UpdateChapter(ChapterCreateDto dto)
    {
        return await _unitOfWork.ChapterRepository.UpdateChapter(dto);
    }

    public async Task<ResponseDto> CreateChapter(ChapterDto dto)
    {
        var isValidName = await _unitOfWork.ChapterRepository.GetAllChapterCheck(dto.ChapterName);
        if (isValidName == false)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Chapter name is already Exits.");
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

    public async Task<ChapterResponseDto> GetChapterByChapterName(string chapterName)
    {
        return await _unitOfWork.ChapterRepository.GetChapterByChapterName(chapterName);
    }

    public async Task<ChapterResponseDto> GetChapterByChapterId(Guid id)
    {
        return await _unitOfWork.ChapterRepository.GetChapterByChapterId(id);
    }
}