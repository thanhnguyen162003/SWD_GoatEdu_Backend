using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.DiscussionInterfaces;

public interface IDiscussionRepository : IRepository<Discussion>
{
    Task<IEnumerable<Discussion>> GetDiscussionByFilters(Guid? userId,DiscussionQueryFilter queryFilter); 
    Task<Discussion?> GetById(Guid guid);
    Task<Discussion?> GetDiscussionByIdAndUserId(Guid discussionId, Guid userId);
    Task SoftDelete(Discussion discussion);
    Task<IEnumerable<Discussion>> GetRandomRelatedDiscussions(int quantity, IEnumerable<string> tagNames);
}