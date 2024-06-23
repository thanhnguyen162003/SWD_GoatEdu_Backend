using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.QuizDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.QuizInterfaces;

public interface IQuizService
{
    Task<ResponseDto> CreateQuizByChapter(Guid chapterId, QuizDto dto);
    Task<ResponseDto> CreateQuizByLesson(Guid lessonId, QuizDto dto);
    Task<ResponseDto> CreateQuizBySubject(Guid subjectId, QuizDto dto);
    Task<ResponseDto> UpdateQuiz(Guid quizId, QuizDto dto);
    Task<ResponseDto> DeleteQuizzes(IEnumerable<Guid> guids);
    Task<PagedList<QuizDto>> GetQuizzesByFilters(QuizQueryFilter queryFilter);
    Task<QuizDto?> GetQuizDetail(Guid quizId);
}