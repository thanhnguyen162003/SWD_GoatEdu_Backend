using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.TheoryInterfaces;

public interface ITheoryRepository : IRepository<Theory>
 {
     Task SoftDelete(IEnumerable<Guid> guids);
     Task<IEnumerable<Theory>> GetTheoryByFilters(Guid? lessonId, TheoryQueryFilter queryFilter);
 }