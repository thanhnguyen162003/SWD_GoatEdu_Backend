using GoatEdu.Core.Interfaces.TheoryInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TheoryRepository : BaseRepository<Theory>, ITheoryRepository
{
    public TheoryRepository(GoatEduContext context) : base(context)
    {
    }
    
    public async Task SoftDelete(IEnumerable<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }

    public async Task<IEnumerable<Theory>> GetTheoryByFilters(Guid? lessonId, TheoryQueryFilter queryFilter)
    {
        var theories = _entities.AsNoTracking().AsQueryable();
        theories = ApplyFilterSortAndSearch(theories, queryFilter, lessonId);
        theories = ApplySorting(theories, queryFilter);
        return await theories.ToListAsync();
    }

    public async Task<bool> TheoryIdExistAsync(Guid theoryId)
    {
        return await _entities.AnyAsync(x => x.Id == theoryId);
    }

    private IQueryable<Theory> ApplyFilterSortAndSearch(IQueryable<Theory> theories, TheoryQueryFilter queryFilter, Guid? lessonId)
    {
        if (lessonId != null)
        {
            theories = theories.Where(x => x.LessonId == lessonId);
        }

        theories = theories.Where(x => x.IsDeleted == false);

        return theories;
    }
    
    private IQueryable<Theory> ApplySorting(IQueryable<Theory> theories, TheoryQueryFilter queryFilter)
    {
        theories = queryFilter.sort.ToLower() switch
        {
            _ => queryFilter.sort_direction.ToLower() == "asc"
                ? theories.OrderBy(x => x.CreatedAt)
                : theories.OrderByDescending(x => x.CreatedAt)
        };
        return theories;
    }
    
    
}