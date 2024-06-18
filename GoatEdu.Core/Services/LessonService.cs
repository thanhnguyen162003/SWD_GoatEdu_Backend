using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class LessonService : ILessonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IValidator<LessonDto> _validator;
    private readonly IMapper _mapper;
    private readonly IClaimsService _claimsService;
    private readonly PaginationOptions _paginationOptions;


    public LessonService(IUnitOfWork unitOfWork, ICurrentTime currentTime, IValidator<LessonDto> validator, IMapper mapper, IClaimsService claimsService, IOptions<PaginationOptions> option)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _validator = validator;
        _mapper = mapper;
        _claimsService = claimsService;
        _paginationOptions = option.Value;
    }

    public async Task<ResponseDto> CreateLesson(LessonDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var mapper = _mapper.Map<Lesson>(dto);

        mapper.CreatedAt = _currentTime.GetCurrentTime();
        mapper.CreatedBy = _claimsService.GetCurrentFullname;
        mapper.IsDeleted = false;

        await _unitOfWork.LessonRepository.AddAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Create Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Create Failed!");
    }

    public async Task<ResponseDto> UpdateLesson(Guid lessonId, LessonDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var lesson = await _unitOfWork.LessonRepository.GetByIdAsync(lessonId);
        if (lesson is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Lesson not found!");
        }

        lesson.LessonName = dto.LessonName ?? lesson.LessonName;
        lesson.LessonBody = dto.LessonBody ?? lesson.LessonBody;
        lesson.LessonMaterial = dto.LessonMaterial ?? lesson.LessonMaterial;
        lesson.DisplayOrder = dto.DisplayOrder ?? lesson.DisplayOrder;
        lesson.UpdatedAt = _currentTime.GetCurrentTime();
        lesson.UpdatedBy = _claimsService.GetCurrentFullname;
        
        _unitOfWork.LessonRepository.Update(lesson);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }

    public async Task<ResponseDto> DeleteLesson(IEnumerable<Guid> guids)
    {
        await _unitOfWork.LessonRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete Failed!");
    }

    public async Task<LessonDto?> GetDetailLessonById(Guid lessonId)
    {
        var result = await _unitOfWork.LessonRepository.GetById(lessonId);
        var mapper = _mapper.Map<LessonDto>(result);
        return mapper;
    }

    public async Task<PagedList<LessonDto>> GetLessonsByChapter(Guid? chapterId, LessonQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var lessons = await _unitOfWork.LessonRepository.GetLessonsByFilters(chapterId, queryFilter);
        if (!lessons.Any()) return new PagedList<LessonDto>(new List<LessonDto>(), 0, 0, 0);

        var mapper = _mapper.Map<IEnumerable<LessonDto>>(lessons);
        
        return PagedList<LessonDto>.Create(mapper, queryFilter.page_number, queryFilter.page_size);
    }
}