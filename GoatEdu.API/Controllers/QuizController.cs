using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using GoatEdu.API.Request.QuizViewModel;
using GoatEdu.API.Response;
using GoatEdu.API.Response.QuizViewModel;
using GoatEdu.API.Response.TheoryViewModel;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.QuizDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.QuizInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/quiz")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;

    public QuizController(IQuizService quizService, IMapper mapper)
    {
        _quizService = quizService;
        _mapper = mapper;
    }
    
    [HttpPost("chapter/{chapterId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateQuizByChapter([FromRoute, Required] Guid chapterId, [FromBody] QuizCreateModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<QuizDto>(model);
            var result = await _quizService.CreateQuizByChapter(chapterId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("lesson/{lessonId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateQuizByLesson([FromRoute, Required] Guid lessonId, [FromBody] QuizCreateModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<QuizDto>(model);
            var result = await _quizService.CreateQuizByLesson(lessonId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("subject/{subjectId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateQuizBySubject([FromRoute, Required] Guid subjectId, [FromBody] QuizCreateModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<QuizDto>(model);
            var result = await _quizService.CreateQuizBySubject(subjectId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("{quizId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateQuiz([FromRoute, Required] Guid quizId, [FromBody] QuizUpdateModel model)
    {
        try
        {
            var mapper = _mapper.Map<QuizDto>(model);
            var result = await _quizService.UpdateQuiz(quizId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> DeleteQuizzes([FromQuery] IEnumerable<Guid> quizIds)
    {
        try
        {
            var result = await _quizService.DeleteQuizzes(quizIds);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{quizId}")]
    public async Task<IActionResult> GetQuizById([Required] Guid quizId)
    {
        try
        {
            var result = await _quizService.GetQuizDetail(quizId);
            var mapper = _mapper.Map<QuizDetailResponseModel>(result);

            return mapper is null
                ? Ok(new ResponseDto(HttpStatusCode.NotFound, "Quiz not found!"))
                : Ok(new ResponseDto(HttpStatusCode.OK, "Found!", mapper));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetQuizzesByFilter([FromQuery, Required] QuizQueryFilter queryFilter)
    {
        try
        {
            var result = await _quizService.GetQuizzesByFilters(queryFilter);
            
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

            var mapper = _mapper.Map<PagedList<QuizResponseModel>>(result);
            
            return Ok(mapper);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}