using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.QuestionQuizInterfaces;

public interface IQuestionQuizRepository : IRepository<QuestionInQuiz>
{
    Task SoftDelete(IEnumerable<Guid> guids);
    Task<List<QuestionInQuiz>> GetQuestionInQuizByIds(Guid quizId, IEnumerable<Guid?> guids);
}