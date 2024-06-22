using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.LessonInterfaces;

public interface ILessonRepository : IRepository<Lesson>
{
    Task SoftDelete(IEnumerable<Guid> guids);
    Task<Lesson?> GetById(Guid lessonId);
    Task<IEnumerable<Lesson>> GetLessonsByFilters(Guid? chapterId, LessonQueryFilter queryFilter); 
    
    // Validation
    Task<bool> LessonIdExistAsync(Guid? lessonId);
}