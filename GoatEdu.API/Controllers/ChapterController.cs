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
    private readonly IMapper _mapper;


    public ChapterController(IChapterService chapterService, IMapper mapper)
    {
        _chapterService = chapterService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ICollection<ChapterResponseModel>> GetAllChapter([FromQuery, Required] ChapterQueryFilter queryFilter)
    {
        var listChapter =  await _chapterService.GetChapters(queryFilter);
        var mapper = _mapper.Map<ICollection<ChapterResponseModel>>(listChapter);
        return mapper;

    }
    
    [HttpGet("{id}")]
    public async Task<ChapterResponseModel> GetChapterById(Guid id)
    {
        var chapter = await _chapterService.GetChapterByChapterId(id);
        var mapper = _mapper.Map<ChapterResponseModel>(chapter);
        return mapper;
    }
    [HttpGet("name")]
    public async Task<ChapterResponseModel> GetChapterByName([FromQuery] string chapterName)
    {
        var chapter = await _chapterService.GetChapterByChapterName(chapterName);
        var mapper = _mapper.Map<ChapterResponseModel>(chapter);
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
        if (!ModelState.IsValid)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", ModelState);
        }
        return await _chapterService.CreateChapter(dto);
    }
    [HttpPut("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> UpdateChapter([FromBody] ChapterCreateModel model, [FromRoute]Guid id)
    {
        var mapper = _mapper.Map<ChapterDto>(model);
        return await _chapterService.UpdateChapter(mapper, id);
    }
}