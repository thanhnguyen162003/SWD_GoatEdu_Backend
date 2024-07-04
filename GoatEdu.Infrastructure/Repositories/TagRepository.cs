using Dapper;
using GoatEdu.Core.Interfaces.TagInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

    public async Task<IEnumerable<Tag>> GetTagByNameAsync(string tagName)
    {
        var query = "SELECT * FROM \"Tag\" WHERE \"tagName\" LIKE '%' || @TagName || '%'";
        var connectionString = _context.Database.GetConnectionString();
        await using var connection = new NpgsqlConnection(connectionString);
        var result = await connection.QueryAsync<Tag>(query, new {TagName = tagName});
        return result;
    }

    public async Task<List<Tag>> GetTagByNames(List<string> tagNames)
    {
        return await _entities.Where(x => tagNames.Any(name => name.Equals(x.TagName)) && x.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Tag>> CheckTagByNamesAsync(List<string?> tagNames)
    {
        var query = "SELECT * FROM \"Tag\" AS t WHERE LOWER(\"tagName\") = ANY(@TagNames) AND t.\"isDeleted\" = false";
        var connectionString = _context.Database.GetConnectionString();
        await using var connection = new NpgsqlConnection(connectionString);
        var result = await connection.QueryAsync<Tag>(query, new {TagNames = tagNames});
        return result;
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