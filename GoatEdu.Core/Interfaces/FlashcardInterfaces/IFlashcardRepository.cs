using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.FlashcardInterfaces;

public interface IFlashcardRepository : IRepository<Flashcard>
{
    Task DisableUnder4TagsFlashcard();
    Task<IEnumerable<Flashcard>> GetFlashcards(FlashcardQueryFilter queryFilter);
    Task<Flashcard> GetFlashcardById(Guid flashcardId);
    Task<IEnumerable<Flashcard>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid id);
    Task<ResponseDto> CreateFlashcard(Flashcard flashcard);
    Task<ResponseDto> UpdateFlashcard(Flashcard flashcard, Guid userId);
    Task<ResponseDto> DeleteFlashcard(Guid flashcardId, Guid userId);
    Task<Flashcard> GetFlashcarDetail(Guid flashcardId);
    Task<IEnumerable<Flashcard>> GetOwnFlashcard(FlashcardQueryFilter queryFilter, Guid userId);

}