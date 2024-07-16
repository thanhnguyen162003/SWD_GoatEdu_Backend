using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.QuizDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.QuizInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class QuizService : IQuizService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;
    private readonly IValidator<QuizDto> _validator;
    private readonly IValidator<QuizQueryFilter> _validatorQuery;
    private readonly PaginationOptions _paginationOptions;

    public QuizService(IUnitOfWork unitOfWork, ICurrentTime currentTime, IMapper mapper, IValidator<QuizDto> validator, IValidator<QuizQueryFilter> validatorQuery, IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
        _validator = validator;
        _validatorQuery = validatorQuery;
        _paginationOptions = options.Value;
    }

    public async Task<ResponseDto> CreateQuizByChapter(Guid chapterId, QuizDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var chapter = await _unitOfWork.ChapterRepository.ChapterIdExistsAsync(chapterId);

        if (chapter is false)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Chapter not found!");
        }
        
        var quiz = _mapper.Map<Quiz>(dto);

        quiz.CreatedAt = _currentTime.GetCurrentTime();
        quiz.ChapterId = chapterId;
        quiz.IsDeleted = false;

        await _unitOfWork.QuizRepository.AddAsync(quiz);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Create Quiz Successfully!", quiz.Id)
            : new ResponseDto(HttpStatusCode.BadRequest, "Create Quiz Failed!");
    }

    public async Task<ResponseDto> CreateQuizByLesson(Guid lessonId, QuizDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var lesson = await _unitOfWork.LessonRepository.LessonIdExistAsync(lessonId);

        if (lesson is false)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Lesson not found!");
        }
        
        var quiz = _mapper.Map<Quiz>(dto);

        quiz.CreatedAt = _currentTime.GetCurrentTime();
        quiz.LessonId = lessonId;
        quiz.IsDeleted = false;

        await _unitOfWork.QuizRepository.AddAsync(quiz);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Create Quiz Successfully!", quiz.Id)
            : new ResponseDto(HttpStatusCode.BadRequest, "Create Quiz Failed!");
    }

    public async Task<ResponseDto> CreateQuizBySubject(Guid subjectId, QuizDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var subject = await _unitOfWork.SubjectRepository.SubjectIdExistAsync(subjectId);

        if (subject is false)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Subject not found!");
        }
        
        var quiz = _mapper.Map<Quiz>(dto);

        quiz.CreatedAt = _currentTime.GetCurrentTime();
        quiz.ChapterId = subjectId;
        quiz.IsDeleted = false;
        
        await _unitOfWork.QuizRepository.AddAsync(quiz);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Create Quiz Successfully!", quiz.Id)
            : new ResponseDto(HttpStatusCode.BadRequest, "Create Quiz Failed!");
    }

    public async Task<ResponseDto> UpdateQuiz(Guid quizId, QuizDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(quizId);
        if (quiz is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Quiz not found!");
        }

        quiz.Quiz1 = dto.Quiz1 ?? quiz.Quiz1;
        quiz.IsRequire = dto.IsRequire ?? quiz.IsRequire;
        quiz.QuizLevel = dto.QuizLevel ?? quiz.QuizLevel;
        quiz.UpdatedAt = _currentTime.GetCurrentTime();
        
        _unitOfWork.QuizRepository.Update(quiz);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update Quiz Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update Quiz Failed!");
    }

    public async Task<ResponseDto> DeleteQuizzes(IEnumerable<Guid> guids)
    {
        await _unitOfWork.QuizRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete Quizzes Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete Quizzes Failed!");
    }

    public async Task<PagedList<QuizDto>> GetQuizzesByFilters(QuizQueryFilter queryFilter)
    {
        var validationResult = await _validatorQuery.ValidateAsync(queryFilter);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            throw new Exception($"Validation Errors: {string.Join(", ", errors)}");
        }
        
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var quizzes = await _unitOfWork.QuizRepository.GetQuizByFilters(queryFilter);
        
        if (!quizzes.Any())
        {
            return new PagedList<QuizDto>(new List<QuizDto>(), 0, 0, 0);
        }
        
        var mapperList = _mapper.Map<List<QuizDto>>(quizzes);
        
        return PagedList<QuizDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<QuizDto?> GetQuizDetail(Guid quizId)
    {
        var quiz = await _unitOfWork.QuizRepository.GetQuizById(quizId);
        var mapper = _mapper.Map<QuizDto>(quiz);
        return mapper;
    }
}