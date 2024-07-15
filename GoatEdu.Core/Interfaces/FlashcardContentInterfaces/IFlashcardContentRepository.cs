using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.FlashcardContentInterfaces;

public interface IFlashcardContentRepository : IRepository<FlashcardContent>
{
    Task<IEnumerable<FlashcardContent>> GetFlashcardContent(FlashcardQueryFilter queryFilter, Guid flashcardId);
    Task<List<FlashcardContent>> GetFlashcardContentByIds(Guid userId, IEnumerable<Guid?> listId);
    Task<ResponseDto> CreateFlashcardContent(List<FlashcardContent> flashcardContents);
    Task<ResponseDto> UpdateFlashcardContent(FlashcardContent flashcard, Guid id);
    Task<ResponseDto> DeleteFlashcardContent(Guid flashcardContentId, Guid userId);
}