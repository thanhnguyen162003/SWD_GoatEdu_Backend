
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
        var discussions = _entities
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Tags)
            .Include(x => x.Answers)
            .AsSplitQuery()
            .AsQueryable();
        discussions = ApplyFilterSortAndSearch(discussions, queryFilter, userId);
        discussions =  ApplySorting(discussions, queryFilter);
        
        return await discussions.ToListAsync();
    }

    public async Task<Discussion?> GetById(Guid guid)
    {
        return await _entities
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Subject)
            .Include(x => x.Tags)
            .Include(x => x.Answers)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == guid && x.IsDeleted == false);
    }

    public async Task<Discussion?> GetDiscussionByIdAndUserId(Guid guid, Guid userId)
    {
        return await _entities.Include(x => x.Tags).AsSplitQuery().FirstOrDefaultAsync(x => x.Id == guid && x.UserId == userId);
    }

    public async Task SoftDelete(List<Guid> guids, Guid userId)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id) && x.UserId == userId).ForEachAsync(a => a.IsDeleted = true);
    }

    public async Task<IEnumerable<Discussion>> GetSignificantDiscussionByFilter(DiscussionQueryFilter queryFilter)
    {
        var discussions = _entities.AsNoTracking().Where(x => x.IsDeleted == false)
            .Include(x => x.Answers)
            .Include(x => x.User)
            .AsSplitQuery()
            .AsQueryable();
        discussions = ApplySorting(discussions, queryFilter);
        return await discussions.ToListAsync();
    }

    public async Task<IEnumerable<Discussion>> GetRandomRelatedDiscussions(int quantity, IEnumerable<string> tagNames)
    {
        return await _context.Discussions
            .Include(x => x.User)
            .Include(x => x.Subject)
            .AsSplitQuery()
            .Include(x => x.Tags)
            .Where(x => x.Tags.Any(t => tagNames.Any(name => name.Equals(t.TagName))))
            .OrderBy(d => Guid.NewGuid())
            .Take(quantity)
            .ToListAsync();
    }

    private IQueryable<Discussion> ApplyFilterSortAndSearch(IQueryable<Discussion> discussions, DiscussionQueryFilter queryFilter, Guid? userId)
    {
        discussions = discussions.Where(x => x.IsDeleted == false);
        
        if (userId != null)
        {
            discussions = discussions
                .Where(x => x.UserId == userId)
                .Include(x => x.User);
        }
        
        if (string.IsNullOrEmpty(queryFilter.status))
        {
            discussions = discussions.Where(x => x.Status.Equals(queryFilter.status));
        }
        
        if (queryFilter.tag_names.Any())
        {
            discussions = discussions.Where(x => x.Tags
                .Any(t => queryFilter.tag_names
                        .Any(name => name.Equals(t.TagName))));
        }
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            discussions = discussions.Where(x => x.DiscussionName.Contains(queryFilter.search));
        }
        return discussions;
    }
    
    private IQueryable<Discussion> ApplySorting(IQueryable<Discussion> discussions, DiscussionQueryFilter queryFilter)
    {
        discussions = queryFilter.sort.ToLower() switch
        {
            "significant" => discussions.OrderByDescending(x=> x.Answers.Count).ThenByDescending(x => x.DiscussionVote),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? discussions.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Id)
                : discussions.OrderBy(x => x.CreatedAt).ThenBy(x => x.Id)
        };
        return discussions;
    }
}