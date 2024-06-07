using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/flashcard_content")]
[ApiController]
public class FlashcardContentController : ControllerBase
{
    private readonly IFlashcardContentService _flashcardContentService;
    private readonly IMapper _mapper;

    public FlashcardContentController(IFlashcardContentService flashcardContentService, IMapper mapper)
    {
        _flashcardContentService = flashcardContentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("{flashcardId}")]
    public async Task<IEnumerable<FlashcardContentDto>> GetFlashcardContents([FromQuery, Required] FlashcardQueryFilter queryFilter, [FromRoute] Guid flashcardId)
    {
        return await _flashcardContentService.GetFlashcards(queryFilter,flashcardId);
        
    }
}