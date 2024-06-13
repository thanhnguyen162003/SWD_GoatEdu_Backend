using System.Net;
using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.RateInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class RateService : IRateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimsService _claimsService;
    public RateService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
    {
        _mapper = mapper;
        _claimsService = claimsService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> RateFlashcard(short rateValue, Guid flashcardId)
    {
        var userId = _claimsService.GetCurrentUserId;
        var flashcard = await _unitOfWork.FlashcardRepository.GetFlashcardById(flashcardId);
        if (flashcard.UserId == userId)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "You cant rate for your flashcard");
        }

        if (rateValue > 5 || rateValue < 0)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "rate value error");
        }
        Rate rate = new Rate()
        {
            UserId = userId,
            FlashcardId = flashcardId,
            RateValue = rateValue,
            CreatedAt = DateTime.Now
        };
        return await _unitOfWork.RateRepository.RateFlashcard(rate);
    }
}