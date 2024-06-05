using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.Enumerations;
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
    private readonly IMapper _mapper;


    public ChapterController(IChapterService chapterService, IValidator<ChapterDto> validator, IMapper mapper)
    {
        _chapterService = chapterService;
        _validator = validator;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ICollection<ChapterResponseDto>> GetAllChapter([FromQuery, Required] ChapterQueryFilter queryFilter)
    {
        var listChapter =  await _chapterService.GetChapters(queryFilter);
        var mapper = _mapper.Map<ICollection<ChapterResponseDto>>(listChapter);
        return mapper;

    }
    
    [HttpGet("{id}")]
    public async Task<ChapterResponseDto> GetChapterById(Guid id)
    {
        var chapter = await _chapterService.GetChapterByChapterId(id);
        var mapper = _mapper.Map<ChapterResponseDto>(chapter);
        return mapper;
    }
    [HttpGet("name")]
    public async Task<ChapterResponseDto> GetChapterByName([FromQuery] string chapterName)
    {
        var chapter = await _chapterService.GetChapterByChapterName(chapterName);
        var mapper = _mapper.Map<ChapterResponseDto>(chapter);
        return mapper;
    }
    [HttpDelete("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> DeleteChapter(Guid id)
    {
        return await _chapterService.DeleteChapter(id);
    }
    [HttpPost]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> CreateChapter([FromBody] ChapterDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}");
        }
        return await _chapterService.CreateChapter(dto);
    }
    [HttpPut("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> UpdateChapter([FromBody] ChapterCreateDto dto, [FromRoute]Guid id)
    {
        var mapper = _mapper.Map<ChapterDto>(dto);
        return await _chapterService.UpdateChapter(mapper, id);
    }
}