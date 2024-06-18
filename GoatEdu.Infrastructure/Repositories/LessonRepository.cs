using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
{
    private readonly GoatEduContext _context;

    public LessonRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task SoftDelete(IEnumerable<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(x => x.IsDeleted = true);
    }

    public async Task<Lesson?> GetById(Guid lessonId)
    {
        return await _entities
            .AsNoTracking()
            .Include(x => x.Theories)
            .Include(x => x.Quizzes)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == lessonId && x.IsDeleted == false);
    }

    public async Task<IEnumerable<Lesson>> GetLessonsByFilters(Guid? chapterId, LessonQueryFilter queryFilter)
    {
        var lessons = _entities
            .AsNoTracking()
            .AsQueryable();
        lessons = ApplyFilterSortAndSearch(lessons, queryFilter, chapterId);
        lessons =  ApplySorting(lessons, queryFilter);
        
        return await lessons.ToListAsync();
    }

    private IQueryable<Lesson> ApplyFilterSortAndSearch(IQueryable<Lesson> lessons, LessonQueryFilter queryFilter, Guid? chapterId)
    {
        if (chapterId != null)
        {
            lessons = lessons.Where(x => x.ChapterId == chapterId);
        }

        lessons = lessons.Where(x => x.IsDeleted == false);

        return lessons;
    }
    
    private IQueryable<Lesson> ApplySorting(IQueryable<Lesson> lessons, LessonQueryFilter queryFilter)
    {
        lessons = queryFilter.sort.ToLower() switch
        {
            _ => queryFilter.sort_direction.ToLower() == "asc"
                ? lessons.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id)
                : lessons.OrderByDescending(x => x.DisplayOrder).ThenBy(x => x.Id)
        };
        return lessons;
    }
}