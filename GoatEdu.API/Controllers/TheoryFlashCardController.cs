using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using GoatEdu.API.Request.TheoryFlashcardViewModel;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.TheoryFlashcardContentInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/theory_flashcard")]
[ApiController]
public class TheoryFlashCardController : ControllerBase
{
    private readonly ITheoryFlashcardContentService _theoryFlashcardContentService;
    private readonly IMapper _mapper;

    public TheoryFlashCardController(ITheoryFlashcardContentService theoryFlashcardContentService, IMapper mapper)
    {
        _theoryFlashcardContentService = theoryFlashcardContentService;
        _mapper = mapper;
    }
    
    [HttpPost("theory/{theoryId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateTheoryFlashcards([FromRoute, Required] Guid theoryId, [FromBody] List<TheoryFlashcardRequestModel> model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<List<TheoryFlashcardContentsDto>>(model);
            var result = await _theoryFlashcardContentService.CreateTheTheoryFlashcardContent(theoryId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("theory/{theoryId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateTheoryFlashcards([FromRoute, Required] Guid theoryId, [FromBody] List<TheoryFlashcardUpdateModel> model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<List<TheoryFlashcardContentsDto>>(model);
            var result = await _theoryFlashcardContentService.UpdateTheTheoryFlashcardContent(theoryId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> DeleteTheoryFlashcards([FromQuery] List<Guid> guids)
    {
        try
        {
            var result = await _theoryFlashcardContentService.DeleteTheTheoryFlashcardContent(guids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("theory/{theoryId}")]
    [Authorize(Roles = $"{UserEnum.MODERATOR}, {UserEnum.TEACHER}, {UserEnum.STUDENT}")]
    public async Task<IActionResult> GetTheoryFlashcardsByTheory([FromRoute, Required] Guid theoryId)
    {
        try
        {
            var result = await _theoryFlashcardContentService.GetTheoryFlashcardContentsByTheory(theoryId);

            if (!result.Any())
            {
                return Ok("Theory not have any flashcard!");
            }

            var mapper = _mapper.Map<IEnumerable<TheoryFlashcardResponseModel>>(result);
            return Ok(mapper);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}