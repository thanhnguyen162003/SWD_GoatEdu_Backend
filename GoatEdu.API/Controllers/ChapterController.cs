using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/chapter")]
[ApiController]
public class ChapterController : ControllerBase
{
    private readonly IChapterService _chapterService;
    private readonly IValidator<ChapterDto> _validator;


    public ChapterController(IChapterService chapterService, IValidator<ChapterDto> validator)
    {
        _chapterService = chapterService;
        _validator = validator;
    }
    [HttpGet]
    public async Task<ICollection<ChapterResponseDto>> GetAllChapter([FromQuery, Required] ChapterQueryFilter queryFilter)
    {
        return await _chapterService.GetChapters(queryFilter);
    }
    
    [HttpGet("id/{id}")]
    public async Task<ChapterResponseDto> GetChapterById(Guid id)
    {
        return await _chapterService.GetChapterByChapterId(id);
    }
    [HttpGet("name")]
    public async Task<ChapterResponseDto> GetChapterByName([FromQuery] string chapterName)
    {
        return await _chapterService.GetChapterByChapterName(chapterName);
    }
    [HttpDelete("id/{id}")]
    [Authorize]
    public async Task<ResponseDto> DeleteChapter(Guid id)
    {
        return await _chapterService.DeleteChapter(id);
    }
    [HttpPost]
    [Authorize]
    public async Task<ResponseDto> CreateChapter([FromBody] ChapterDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}");
        }
        return await _chapterService.CreateChapter(dto);
    }
    [HttpPut("id/{id}")]
    [Authorize]
    public async Task<ResponseDto> UpdateChapter([FromBody] ChapterCreateDto dto)
    {
        return await _chapterService.UpdateChapter(dto);
    }
}