using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Request;
using GoatEdu.Core.DTOs.QuestionInQuizDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.QuestionQuizInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionQuizController : ControllerBase
{
    private readonly IQuestionQuizService _questionQuizService;
    private readonly IMapper _mapper;

    public QuestionQuizController(IQuestionQuizService questionQuizService, IMapper mapper)
    {
        _questionQuizService = questionQuizService;
        _mapper = mapper;
    }
    
    [HttpPost("quiz/{quizId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateQuestionInQuiz([FromRoute, Required] Guid chapterId, [FromBody] List<QuestionInQuizCreateModel>  model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<List<QuestionInQuizDto>>(model);
            var result = await _questionQuizService.CreateQuestionQuiz(chapterId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("quiz/{quizId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateQuestionInQuiz([FromRoute, Required] Guid quizId, [FromBody] List<QuestionInQuizUpdateModel> model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<List<QuestionInQuizDto>>(model);
            var result = await _questionQuizService.UpdateQuestionQuiz(quizId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> DeleteQuestionInQuiz([FromQuery] IEnumerable<Guid> ids)
    {
        try
        {
            var result = await _questionQuizService.DeleteQuestionQuiz(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}