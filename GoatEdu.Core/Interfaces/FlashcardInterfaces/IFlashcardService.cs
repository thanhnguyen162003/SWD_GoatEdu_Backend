using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.FlashcardInterfaces;

public interface IFlashcardService
{
    Task<IEnumerable<FlashcardResponseDto>> GetFlashcards(FlashcardQueryFilter queryFilter);

    Task<IEnumerable<FlashcardResponseDto>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid subjectId);
    Task<ResponseDto> CreateFlashcard(FlashcardCreateDto flashcard, Guid subjectId);
    Task<ResponseDto> UpdateFlashcard(FlashcardUpdateDto flashcard);
    Task<ResponseDto> DeleteFlashcard(Guid flashcardId);
}