using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.QuestionInQuizDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.QuestionQuizInterfaces;

public interface IQuestionQuizService
{
    Task<ResponseDto> CreateQuestionQuiz(Guid quizId, List<QuestionInQuizDto> dtos);
    Task<ResponseDto> UpdateQuestionQuiz(Guid quizId,List<QuestionInQuizDto> dtos);
    Task<ResponseDto> DeleteQuestionQuiz(IEnumerable<Guid> guids);
}