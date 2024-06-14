using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.RateInterfaces;

public interface IRateRepository
{
    Task<ResponseDto> RateFlashcard(Rate rate);
    Task<int> GetNumberRating(Guid flashcardId);
    
}