using GoatEdu.Core.Interfaces.QuizInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
{
    public QuizRepository(GoatEduContext context) : base(context)
    {
    }

    public async Task SoftDelete(IEnumerable<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }

    public async Task<IEnumerable<Quiz>> GetQuizByFilters(QuizQueryFilter queryFilter)
    {
        var quizzes = _entities.Include(x => x.QuestionInQuizzes.Where(q => q.IsDeleted == false)).AsNoTracking().AsSplitQuery().AsQueryable();
        quizzes = ApplyFilterSortAndSearch(quizzes, queryFilter);
        quizzes = ApplySorting(quizzes, queryFilter);
        return await quizzes.ToListAsync();
    }

    public async Task<Quiz?> GetQuizById(Guid quizId)
    {
        return await _entities
            .Include(x => x.QuestionInQuizzes.Where(q => q.IsDeleted == false))
            .FirstOrDefaultAsync(x => x.Id == quizId);
    }

    public async Task<bool> QuizIdExistAsync(Guid quizId)
    {
        return await _entities.AnyAsync(x => x.Id == quizId);
    }

    private IQueryable<Quiz> ApplyFilterSortAndSearch(IQueryable<Quiz> quizzes, QuizQueryFilter queryFilter)
    {
        quizzes = quizzes.Where(x => x.IsDeleted == false);

        if (queryFilter.id != null)
        {
            quizzes = queryFilter.type.ToLower() switch
            {
                "lesson" => quizzes.Where(x => x.LessonId == queryFilter.id),
                "chapter" => quizzes.Where(x => x.ChapterId == queryFilter.id),
                "subject" => quizzes.Where(x => x.SubjectId == queryFilter.id),
                _ => quizzes
            };
        }
        
        return quizzes;
    }
    
    private IQueryable<Quiz> ApplySorting(IQueryable<Quiz> quizzes, QuizQueryFilter queryFilter)
    {
        quizzes = queryFilter.sort.ToLower() switch
        {
            _ => queryFilter.sort_direction.ToLower() == "asc"
                ? quizzes.OrderBy(x => x.QuizLevel).ThenBy(x => x.Quiz1).ThenBy(x => x.Id)
                : quizzes.OrderByDescending(x => x.QuizLevel).ThenBy(x => x.Quiz1).ThenBy(x => x.Id)
        };
        return quizzes;
    }
}