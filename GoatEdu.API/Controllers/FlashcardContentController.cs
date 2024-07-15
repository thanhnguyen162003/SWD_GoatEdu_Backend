using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
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
    [HttpPost("{flashcardId}")]
    [Authorize]
    public async Task<ResponseDto> CreateFlashcardContents(Guid flashcardId, [FromBody] List<FlashcardContentDto> listFlashcardContent)
    {
        return await _flashcardContentService.CreateFlashcardContent(listFlashcardContent, flashcardId);
    }
    
    [HttpPatch]
    [Route("{id}")]
    [Authorize]
    public async Task<ResponseDto> UpdateFlashcard([FromRoute] Guid id, [FromBody] FlashcardContentRequest flashcardUpdateModel)
    {
        var mapper = _mapper.Map<FlashcardContentDto>(flashcardUpdateModel);
        return await _flashcardContentService.UpdateFlashcardContent(mapper, id);
    }
    
    [HttpPatch]
    [Route("flashcard/{flashcardId}")]
    [Authorize]
    public async Task<ResponseDto> UpdateFlashcards([FromRoute,Required] Guid flashcardId, [FromBody] List<FlashcardContentRequest> flashcardUpdateModels)
    {
        var mapper = _mapper.Map<List<FlashcardContentDto>>(flashcardUpdateModels);
        return await _flashcardContentService.UpdateFlashcardContents(flashcardId, mapper);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<ResponseDto> DeleteFlashcardContent([FromRoute] Guid id)
    {
        return await _flashcardContentService.DeleteFlashcardContent(id);
    }
}