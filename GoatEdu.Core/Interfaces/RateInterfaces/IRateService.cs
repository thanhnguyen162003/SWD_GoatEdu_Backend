using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.RateInterfaces;

public interface IRateService
{
    Task<ResponseDto> RateFlashcard(short rateValue, Guid flashcardId);
}