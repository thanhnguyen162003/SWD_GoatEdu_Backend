using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.FlashcardInterfaces;

public interface IFlashcardRepository : IRepository<Flashcard>
{
    Task<List<Flashcard>> GetTwoTagFlashcard();
    Task<IEnumerable<Flashcard>> GetFlashcards(FlashcardQueryFilter queryFilter);
    Task<Flashcard> GetFlashcardById(Guid flashcardId);
    Task<IEnumerable<Flashcard>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid id);
    Task<ResponseDto> CreateFlashcard(Flashcard flashcard);
    Task<ResponseDto> UpdateFlashcard(Flashcard flashcard, Guid userId);
    Task<ResponseDto> DeleteFlashcard(Guid flashcardId, Guid userId);

}