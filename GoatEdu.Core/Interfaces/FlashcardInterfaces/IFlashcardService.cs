using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.FlashcardInterfaces;

public interface IFlashcardService
{
    Task<IEnumerable<FlashcardDto>> GetFlashcards(FlashcardQueryFilter queryFilter);

    Task<IEnumerable<FlashcardDto>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid subjectId);
    Task<ResponseDto> CreateFlashcard(FlashcardDto flashcard, Guid subjectId);
    Task<ResponseDto> UpdateFlashcard(FlashcardDto flashcard, Guid id);
    Task<ResponseDto> DeleteFlashcard(Guid flashcardId);
    Task<FlashcardDto> GetFlashcarDetail(Guid flashcardId);
}