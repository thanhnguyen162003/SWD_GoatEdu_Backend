using GoatEdu.Core.Interfaces.AnswerInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
{
    public AnswerRepository(GoatEduContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Answer>> GetAnswersByDiscussionIdFilters(Guid guid, AnswerQueryFilter queryFilter)
    {
        var answers = _entities.AsNoTracking()
            .Where(x => x.QuestionId == guid).
            Include(x => x.User).AsQueryable();
        answers = ApplyFilterSort(answers);
        answers = ApplySorting(answers, queryFilter);
        return await answers.AsSplitQuery().ToListAsync();
    }

    public async Task<Answer?> GetByIdAndUserId(Guid guid, Guid userId)
    {
        return await _entities.SingleOrDefaultAsync(x => x.Id == guid && x.UserId == userId);
    }
    
    private IQueryable<Answer> ApplyFilterSort(IQueryable<Answer> answers)
    {
        return answers.Where(x => x.IsDeleted == false);
    }
    
    private IQueryable<Answer> ApplySorting(IQueryable<Answer> answers, AnswerQueryFilter queryFilter)
    {

        return queryFilter.sort_direction.ToLower() == "desc"
            ? answers.OrderByDescending(x => x.CreatedAt)
            : answers.OrderBy(x => x.CreatedAt);
    }
}