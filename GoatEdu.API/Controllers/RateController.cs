using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.RateInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/rate")]
[ApiController]
[Authorize]
public class RateController : ControllerBase
{
    private readonly IRateService _rateService;

    public RateController(IRateService rateService)
    {
        _rateService = rateService;
    }
    
    [HttpPost("{flashcardId}/{rate}")]
    public async Task<ResponseDto> CreateUser([FromRoute] Guid flashcardId, [FromRoute] short rate)
    {
        return await _rateService.RateFlashcard(rate, flashcardId);
    }

    [HttpGet("{flashcardId}/user")]
    public async Task<ResponseDto> GetRatingUser([FromRoute] Guid flashcardId)
    {
        return await _rateService.GetUserRateFlashcard(flashcardId);
    }
    
    // this code check if user rate flashcard already or not
    [HttpGet("user/{flashcardId}")]
    public async Task<bool> IsUserRated([FromRoute] Guid flashcardId)
    {
        return await _rateService.IsUserRate(flashcardId);
    }
}