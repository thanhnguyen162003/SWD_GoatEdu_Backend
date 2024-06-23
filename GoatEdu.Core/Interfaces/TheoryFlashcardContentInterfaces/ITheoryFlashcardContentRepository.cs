using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.TheoryFlashcardContentInterfaces;

public interface ITheoryFlashcardContentRepository : IRepository<TheoryFlashCardContent>
{
    Task<List<TheoryFlashCardContent>> GetTheoryFlashCardContentByIds(Guid theoryId, IEnumerable<Guid?> guids);
    Task SoftDelete(IEnumerable<Guid> guids);
    Task<IEnumerable<TheoryFlashCardContent>> GetTheoryFlashCardContentByTheory(Guid theoryId, string role);
}