using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/flashcard")]
[ApiController]
public class FlashCardController : ControllerBase
{

    private readonly IFlashcardService _flashcardService;

    public FlashCardController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }
    [HttpGet]
    public async Task<IEnumerable<FlashcardResponseDto>> GetFlashcards([FromQuery, Required] FlashcardQueryFilter queryFilter)
    {
        return await _flashcardService.GetFlashcards(queryFilter);
    }
    [HttpGet]
    [Route("subject/{subjectId}")]
    public async Task<IEnumerable<FlashcardResponseDto>> GetFlashcardsSubject([FromQuery, Required] FlashcardQueryFilter queryFilter, [FromRoute] Guid subjectId)
    {
        return await _flashcardService.GetFlashcardsBySubject(queryFilter,subjectId);
    }
    
    [HttpPost]
    [Route("subject/{subjectId}")]
    public async Task<ResponseDto> CreateFlashcard([FromBody] FlashcardCreateDto dto, [FromRoute] Guid subjectId)
    {
        return await _flashcardService.CreateFlashcard(dto,subjectId);
    }
}