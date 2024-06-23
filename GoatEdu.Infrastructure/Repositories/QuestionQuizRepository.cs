using GoatEdu.Core.Interfaces.QuestionQuizInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QuestionQuizRepository : BaseRepository<QuestionInQuiz>, IQuestionQuizRepository
{
    public QuestionQuizRepository(GoatEduContext context) : base(context)
    {
    }

    public async Task SoftDelete(IEnumerable<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }

    public async Task<List<QuestionInQuiz>> GetQuestionInQuizByIds(Guid quizId, IEnumerable<Guid?> guids)
    {
        return await _entities.Where(x => guids.Contains(x.Id) && x.QuizId == quizId).ToListAsync();
    }
}