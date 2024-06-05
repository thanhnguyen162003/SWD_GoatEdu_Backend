using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/flashcard")]
[ApiController]
public class FlashCardController : ControllerBase
{

    private readonly IFlashcardService _flashcardService;
    private readonly IMapper _mapper;

    public FlashCardController(IFlashcardService flashcardService, IMapper mapper)
    {
        _flashcardService = flashcardService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<FlashcardResponseDto>> GetFlashcards([FromQuery, Required] FlashcardQueryFilter queryFilter)
    {
        var flashcards =  await _flashcardService.GetFlashcards(queryFilter);
        var mapper = _mapper.Map<IEnumerable<FlashcardResponseDto>>(flashcards);
        return mapper;
    }
    [HttpGet]
    [Route("subject/{subjectId}")]
    public async Task<IEnumerable<FlashcardResponseDto>> GetFlashcardsSubject([FromQuery, Required] FlashcardQueryFilter queryFilter, [FromRoute] Guid subjectId)
    {
        var flashcards =  await _flashcardService.GetFlashcardsBySubject(queryFilter,subjectId);
        var mapper = _mapper.Map<IEnumerable<FlashcardResponseDto>>(flashcards);
        return mapper;
    }
    
    [HttpPost]
    [Authorize]
    [Route("subject/{subjectId}")]
    public async Task<ResponseDto> CreateFlashcard([FromBody] FlashcardCreateDto dto, [FromRoute] Guid subjectId)
    {
        var mapper = _mapper.Map<FlashcardDto>(dto);
        return await _flashcardService.CreateFlashcard(mapper,subjectId);
    }
}