using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.TheoryFlashcardContentInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class TheoryFlashcardContentService : ITheoryFlashcardContentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IValidator<TheoryFlashcardContentsDto> _validator;
    private readonly IMapper _mapper;
    private readonly IClaimsService _claimsService;

    public TheoryFlashcardContentService(IUnitOfWork unitOfWork, ICurrentTime currentTime, IValidator<TheoryFlashcardContentsDto> validator, IMapper mapper, IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _validator = validator;
        _mapper = mapper;
        _claimsService = claimsService;
    }

    public async Task<ResponseDto> CreateTheTheoryFlashcardContent(Guid theoryId, List<TheoryFlashcardContentsDto> dtos)
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

        var theory = await _unitOfWork.TheoryRepository.GetByIdAsync(theoryId);
        if (theory is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Theory not found!");
        }

        var mapper = dtos.Select(x =>
        {
            var theoryFlashcard = _mapper.Map<TheoryFlashCardContent>(x);
            theoryFlashcard.CreatedAt = _currentTime.GetCurrentTime();
            theoryFlashcard.Status = StatusConstraint.OPEN;
            theoryFlashcard.TheoryId = theoryId;
            theoryFlashcard.IsDeleted = false;
            return theoryFlashcard;
        }).ToList();

        await _unitOfWork.TheoryFlashcardContentRepository.AddRangeAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Create TheoryFlashcard Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Create TheoryFlashcard Failed!");
    }

    public async Task<ResponseDto> UpdateTheTheoryFlashcardContent(Guid theoryId, List<TheoryFlashcardContentsDto> dtos)
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
        
        var theory = await _unitOfWork.TheoryRepository.GetByIdAsync(theoryId);
        if (theory is null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Theory not found!");
        }

        var guids = dtos.Select(x => x.Id);
        var theoryFlashcards = await _unitOfWork.TheoryFlashcardContentRepository.GetTheoryFlashCardContentByIds(guids);

        if (!theoryFlashcards.Any())
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Theory not have any flashcards to update!");
        }
        
        foreach (var data in theoryFlashcards)
        {
            var dto = dtos.FirstOrDefault(x => x.Id == data.Id);
            data.Question = dto.Question ?? data.Question;
            data.Answer = dto.Answer ?? data.Answer;
            data.Status = dto.Status ?? data.Status;
            data.UpdatedAt = _currentTime.GetCurrentTime();
        }

        _unitOfWork.TheoryFlashcardContentRepository.UpdateRange(theoryFlashcards);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Update TheoryFlashcards Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Update TheoryFlashcards Failed!");
    }

    public async Task<ResponseDto> DeleteTheTheoryFlashcardContent(List<Guid> guids)
    {
        await _unitOfWork.TheoryFlashcardContentRepository.SoftDelete(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Delete TheoryFlashcards Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Delete TheoryFlashcards Failed!");
    }

    public async Task<IEnumerable<TheoryFlashcardContentsDto>> GetTheoryFlashcardContentsByTheory(Guid theoryId)
    {
        var role = _claimsService.GetRole;
        var theoryFlashcards =
            await _unitOfWork.TheoryFlashcardContentRepository.GetTheoryFlashCardContentByTheory(theoryId, role);
        if (!theoryFlashcards.Any())
        {
            return Enumerable.Empty<TheoryFlashcardContentsDto>();
        }

        var mapper = _mapper.Map<IEnumerable<TheoryFlashcardContentsDto>>(theoryFlashcards);
        return mapper;
    }
}