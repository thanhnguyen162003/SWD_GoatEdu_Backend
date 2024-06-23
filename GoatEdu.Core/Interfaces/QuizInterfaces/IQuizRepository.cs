using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.QuizInterfaces;

public interface IQuizRepository : IRepository<Quiz>
{
    Task SoftDelete(IEnumerable<Guid> guids);
    Task<IEnumerable<Quiz>> GetQuizByFilters(QuizQueryFilter queryFilter);
    Task<Quiz?> GetQuizById(Guid quizId);
    Task<bool> QuizIdExistAsync(Guid quizId);

}