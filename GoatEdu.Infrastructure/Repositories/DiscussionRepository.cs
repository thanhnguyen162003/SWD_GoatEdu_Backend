using GoatEdu.Core.Interfaces.DiscussionInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DiscussionRepository : BaseRepository<Discussion>, IDiscussionRepository
{
    private readonly GoatEduContext _context;
    
    public DiscussionRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Discussion>> GetDiscussionByFilters(Guid? userId, DiscussionQueryFilter queryFilter)
    {
        var discussions = _entities.AsQueryable();
        discussions = ApplyFilterSortAndSearch(discussions, queryFilter, userId);
        discussions = ApplySorting(discussions, queryFilter);
        return await discussions.ToListAsync(); 
    }

    public async Task<Discussion?> GetById(Guid guid)
    {
        return await _entities
            .Include(x => x.User)
            .Include(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == guid && x.IsDeleted == false);
    }

    public async Task SoftDelete(List<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }
    
    private IQueryable<Discussion> ApplyFilterSortAndSearch(IQueryable<Discussion> discussions, DiscussionQueryFilter queryFilter, Guid? userId)
    {
        if (userId != null)
        {
            discussions = discussions
                .Where(x => x.UserId == userId)
                .Include(x => x.User);
        }
        
        discussions = discussions.Where(x => x.IsDeleted == false);
        
        if (queryFilter.TagNames.Any())
        {
            discussions = discussions.Where(x => x.Tags
                .Any(t => queryFilter.TagNames
                        .Any(name => name.Equals(t.TagName))));
        }
        
        if (!string.IsNullOrEmpty(queryFilter.Search))
        {
            discussions = discussions.Where(x => x.DiscussionName.Contains(queryFilter.Search));
        }
        return discussions;
    }
    
    private IQueryable<Discussion> ApplySorting(IQueryable<Discussion> discussions, DiscussionQueryFilter queryFilter)
    {
        discussions = queryFilter.Sort.ToLower() switch
        {
            _ => queryFilter.SortDirection.ToLower() == "desc"
                ? discussions.OrderByDescending(x => x.CreatedAt)
                : discussions.OrderBy(x => x.CreatedAt)
        };
        return discussions;
    }
}