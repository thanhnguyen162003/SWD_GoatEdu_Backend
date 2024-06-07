using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.FlashcardContentInterfaces;

public interface IFlashcardContentService
{
    Task<IEnumerable<FlashcardContentDto>> GetFlashcards(FlashcardQueryFilter queryFilter, Guid flashcardId);
}