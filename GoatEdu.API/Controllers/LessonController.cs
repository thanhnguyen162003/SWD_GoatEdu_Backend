using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using GoatEdu.API.Request.LessonViewModel;
using GoatEdu.API.Response.LessonViewModel;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/lesson")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;
    private readonly IMapper _mapper;

    public LessonController(ILessonService lessonService, IMapper mapper)
    {
        _lessonService = lessonService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateLesson([FromBody] LessonRequestModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapper = _mapper.Map<LessonDto>(model);
            var result = await _lessonService.CreateLesson(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }
    
    [HttpPatch ("{lessionId}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateLesson([FromRoute, Required] Guid lessonnId, [FromBody] LessonUpdateModel model)
    {
        try
        {
            var mapper = _mapper.Map<LessonDto>(model);
            var result = await _lessonService.UpdateLesson(lessonnId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> DeleteLesson([FromQuery, Required] IEnumerable<Guid> lessonnId)
    {
        try
        {
            var result = await _lessonService.DeleteLesson(lessonnId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{lessonId}")]
    [Authorize]
    public async Task<ResponseDto> GetDetailLesson([FromRoute, Required] Guid lessonId)
    {
        try
        {
            var result = await _lessonService.GetDetailLessonById(lessonId);
            var mapper = _mapper.Map<LessonDetailResponseModel>(result);
            return mapper is null
                ? new ResponseDto(HttpStatusCode.NotFound, "Lesson not found")
                : new ResponseDto(HttpStatusCode.OK, "Found!", mapper);
        }
        catch (Exception e)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Error", e.Message);
        }
    }
    
    [HttpGet("{chapterId}")]
    [Authorize]
    public async Task<IActionResult> GetLessonsByChapter([FromRoute, Required] Guid chapterId, [FromQuery, Required] LessonQueryFilter queryFilter)
    {
        try
        {
            var result = await _lessonService.GetLessonsByChapter(chapterId, queryFilter);
            var metadata = new Metadata
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            var mapper = _mapper.Map<PagedList<LessonResponseModel>>(result);

            return Ok(mapper);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}