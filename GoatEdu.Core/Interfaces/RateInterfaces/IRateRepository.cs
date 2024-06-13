using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.RateInterfaces;

public interface IRateRepository
{
    Task<ResponseDto> RateFlashcard(Rate rate);
    Task<bool> IsRated(Guid userId, Guid flashcardId);
    
}