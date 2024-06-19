using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.LessonInterfaces;

public interface ILessonService
{
    Task<ResponseDto> CreateLesson(LessonDto dto);
    Task<ResponseDto> UpdateLesson(Guid lessonId, LessonDto dto);
    Task<ResponseDto> DeleteLesson(IEnumerable<Guid> guids);
    Task<LessonDto?> GetDetailLessonById(Guid lessonId);
    Task<PagedList<LessonDto>> GetLessonsByChapter(Guid? chapterId, LessonQueryFilter queryFilter);
    
}