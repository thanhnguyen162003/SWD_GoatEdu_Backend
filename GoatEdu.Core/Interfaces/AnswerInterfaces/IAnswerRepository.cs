using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.AnswerInterfaces;

public interface IAnswerRepository : IRepository<Answer>
{
    Task<IEnumerable<Answer>> GetAnswersByDiscussionIdFilters(Guid guid, AnswerQueryFilter queryFilter);
    Task<Answer?> GetByIdAndUserId(Guid guid, Guid userId);
}