using GoatEdu.Core.Interfaces.TagInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    private readonly GoatEduContext _context;
    
    public TagRepository(GoatEduContext context) : base(context)
    {
        _context = context;
        
    }

    public async Task<List<Tag>> GetTagByFilters(TagQueryFilter queryFilter)
    {
        var tags = _entities.AsNoTracking().AsQueryable();
        tags = ApplyFilterSortAndSearch(tags, queryFilter);
        tags = ApplySorting(tags, queryFilter);
        return await tags.ToListAsync();
    }

    public async Task<IEnumerable<Tag?>> GetTagNameByNameAsync(IEnumerable<string?> tagName)
    {
        return await _entities.Where(x => tagName.Any(name => name.Equals(x.TagName))).ToListAsync();
    }
    
    public async Task SoftDelete(List<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }
    
    private IQueryable<Tag> ApplyFilterSortAndSearch(IQueryable<Tag> tags, TagQueryFilter queryFilter)
    {
        tags = tags.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            tags = tags.Where(x => x.TagName.Contains(queryFilter.search));
        }
        return tags;
    }
    
    private IQueryable<Tag> ApplySorting(IQueryable<Tag> tags, TagQueryFilter queryFilter)
    {
        tags = queryFilter.sort.ToLower() switch
        {
            "date" => queryFilter.sort_direction.ToLower() == "desc"
                ? tags.OrderByDescending(x => x.CreatedAt)
                : tags.OrderBy(x => x.CreatedAt).ThenBy(x => x.TagName),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? tags.OrderByDescending(x => x.TagName)
                : tags.OrderBy(x => x.TagName)
        };
        return tags;
    }
}