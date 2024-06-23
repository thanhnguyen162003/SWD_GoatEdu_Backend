using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.QuestionInQuizDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.QuestionQuizInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class QuestionQuizService : IQuestionQuizService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;
    private readonly IValidator<QuestionInQuizDto> _validator;

    public QuestionQuizService(IUnitOfWork unitOfWork, ICurrentTime currentTime, IMapper mapper, IValidator<QuestionInQuizDto> validator)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ResponseDto> CreateQuestionQuiz(Guid quizId, List<QuestionInQuizDto> dtos)
    {
        foreach (var data in dtos)
        {
            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
            }
        }

        var quiz = await _unitOfWork.QuizRepository.QuizIdExistAsync(quizId);
        if (quiz is false)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Quiz not found!");
        }
        
        var mapper = dtos.Select(x =>
        {
            var questionInQuiz = _mapper.Map<QuestionInQuiz>(x);
            questionInQuiz.QuizId = quizId;
            questionInQuiz.CreatedAt = _currentTime.GetCurrentTime();
            questionInQuiz.IsDeleted = false;
            return questionInQuiz;
        }).ToList();

        await _unitOfWork.QuestionQuizRepository.AddRangeAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Create Question Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Create Question Failed!");
    }

    public async Task<ResponseDto> UpdateQuestionQuiz(Guid quizId, List<QuestionInQuizDto> dtos)
    {
        foreach (var data in dtos)
        {
            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
            }
        }
        
        var quiz = await _unitOfWork.QuizRepository.QuizIdExistAsync(quizId);
        if (quiz is false)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Quiz not found!");
        }

        var guids = dtos.Select(x => x.Id);
        var questions = await _unitOfWork.QuestionQuizRepository.GetQuestionInQuizByIds(quizId, guids);

        if (!questions.Any())
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Quiz not have any question to update!");
        }
        
        foreach (var data in questions)
        {
            var dto = dtos.FirstOrDefault(x => x.Id == data.Id);
            data.QuizQuestion = dto.QuizQuestion ?? data.QuizQuestion;
            data.QuizAnswer1 = dto.QuizAnswer1 ?? data.QuizAnswer1;
            data.QuizAnswer2 = dto.QuizAnswer2 ?? data.QuizAnswer2;
            data.QuizAnswer3 = dto.QuizAnswer3 ?? data.QuizAnswer3;
            data.QuizCorrect = dto.QuizCorrect ?? data.QuizCorrect;
            data.UpdatedAt = _currentTime.GetCurrentTime();
        }

        _unitOfWork.QuestionQuizRepository.UpdateRange(questions);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update Questions Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update Questions Failed!");
    }

    public async Task<ResponseDto> DeleteQuestionQuiz(IEnumerable<Guid> guids)
    {
        await _unitOfWork.QuestionQuizRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete Questions Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete Questions Failed!");
    }
}