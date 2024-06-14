using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.RateInterfaces;

public interface IRateRepository
{
    Task<ResponseDto> RateFlashcard(Rate rate);
    Task GetNumberRating();
    Task<ResponseDto> GetUserRateFlashcard(Guid userId, Guid flashcardId);


}