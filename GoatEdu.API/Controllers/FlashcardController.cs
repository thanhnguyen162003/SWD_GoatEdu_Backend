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
public class FlashcardController : ControllerBase
{
    private readonly IFlashcardService _flashcardService;
    private readonly IMapper _mapper;

    public FlashcardController(IFlashcardService flashcardService, IMapper mapper)
    {
        _flashcardService = flashcardService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<FlashcardResponseModel>> GetFlashcards([FromQuery, Required] FlashcardQueryFilter queryFilter)
    {
        var flashcards =  await _flashcardService.GetFlashcards(queryFilter);
        var mapper = _mapper.Map<IEnumerable<FlashcardResponseModel>>(flashcards);
        return mapper;
    }
    [HttpGet("user")]
    public async Task<IEnumerable<FlashcardResponseModel>> GetOwnFlashcards([FromQuery, Required] FlashcardQueryFilter queryFilter)
    {
        var flashcards =  await _flashcardService.GetOwnFlashcard(queryFilter);
        var mapper = _mapper.Map<IEnumerable<FlashcardResponseModel>>(flashcards);
        return mapper;
    }
    [HttpGet]
    [Route("subject/{subjectId}")]
    public async Task<IEnumerable<FlashcardResponseModel>> GetFlashcardsSubject([FromQuery, Required] FlashcardQueryFilter queryFilter, [FromRoute] Guid subjectId)
    {
        var flashcards =  await _flashcardService.GetFlashcardsBySubject(queryFilter,subjectId);
        var mapper = _mapper.Map<IEnumerable<FlashcardResponseModel>>(flashcards);
        return mapper;
    }
    [HttpGet]
    [Route("{flashcardId}")]
    public async Task<FlashcardDetailResponseModel> GetFlashcardsSubject([FromRoute] Guid flashcardId)
    {
        var flashcards =  await _flashcardService.GetFlashcarDetail(flashcardId);
        var mapper = _mapper.Map<FlashcardDetailResponseModel>(flashcards);
        return mapper;
    }
    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<ResponseDto> DeleteFlashcard([FromRoute] Guid id)
    {
        return await _flashcardService.DeleteFlashcard(id);
    }
    [HttpPut]
    [Route("{id}")]
    [Authorize]
    public async Task<ResponseDto> UpdateFlashcard([FromRoute] Guid id, [FromBody] FlashcardUpdateModel flashcardUpdateModel)
    {
        var mapper = _mapper.Map<FlashcardDto>(flashcardUpdateModel);
        return await _flashcardService.UpdateFlashcard(mapper, id);
    }
    
    [HttpPost]
    [Authorize]
    [Route("subject/{subjectId}")]
    public async Task<ResponseDto> CreateFlashcard([FromBody] FlashcardCreateModel model, [FromRoute] Guid subjectId)
    {
        var mapper = _mapper.Map<FlashcardDto>(model);
        return await _flashcardService.CreateFlashcard(mapper,subjectId);
    }
}