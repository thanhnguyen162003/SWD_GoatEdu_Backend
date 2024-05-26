using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.FlashcardInterfaces;

public interface IFlashcardRepository : IRepository<Flashcard>
{
    Task<List<Flashcard>> GetTwoTagFlashcard();
}