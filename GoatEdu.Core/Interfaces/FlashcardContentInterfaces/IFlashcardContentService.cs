using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.FlashcardContentInterfaces;

public interface IFlashcardContentService
{
    Task<IEnumerable<FlashcardContentDto>> GetFlashcards(FlashcardQueryFilter queryFilter, Guid flashcardId);
    Task<ResponseDto> CreateFlashcardContent(List<FlashcardContentDto> listFlashcardContent, Guid flashcardId);
    Task<ResponseDto> UpdateFlashcardContent(FlashcardContentDto flashcard, Guid id);
    Task<ResponseDto> DeleteFlashcardContent(Guid flashcardContentId);
}