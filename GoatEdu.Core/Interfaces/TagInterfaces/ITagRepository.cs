using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.TagInterfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<List<Tag>> GetTagByFilters(TagQueryFilter queryFilter);
    Task<IEnumerable<Tag?>> GetTagByNameAsync(string tagName);
    Task<IEnumerable<Tag?>> GetTagByNamesAsync(List<string?> tagNames);
    Task SoftDelete(List<Guid> guids);
}