using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.TagInterfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<List<Tag>> GetTagByFilters(TagQueryFilter queryFilter); 

    Task<IEnumerable<Tag?>> GetTagNameByNameAsync(List<string?> tagName);
    Task SoftDelete(List<Guid> guids);
}