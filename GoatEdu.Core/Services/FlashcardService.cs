using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly IClaimsService _claimsService;

    public FlashcardService(IUnitOfWork unitOfWork,IMapper mapper, IOptions<PaginationOptions> paginationOptions,IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
        _claimsService = claimsService;
    }
    
    public async Task<IEnumerable<FlashcardResponseDto>> GetFlashcards(FlashcardQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listFlashcard = await _unitOfWork.FlashcardRepository.GetFlashcards(queryFilter);
        
        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardResponseDto>(new List<FlashcardResponseDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<FlashcardResponseDto>>(listFlashcard);
        return PagedList<FlashcardResponseDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<IEnumerable<FlashcardResponseDto>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid subjectId)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listFlashcard = await _unitOfWork.FlashcardRepository.GetFlashcardsBySubject(queryFilter,subjectId);
        
        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardResponseDto>(new List<FlashcardResponseDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<FlashcardResponseDto>>(listFlashcard);
        return PagedList<FlashcardResponseDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> CreateFlashcard(FlashcardCreateDto flashcard, Guid subjectId)
    {
        var userId = _claimsService.GetCurrentUserId;
        var fullname = _claimsService.GetCurrentFullname;

        var newFlashcard = new Flashcard()
        {
            FlashcardName = flashcard.flashcardName,
            FlashcardDescription = flashcard.flashcardDescription,
            UserId = userId,
            SubjectId = subjectId,
            Status = StatusConstraint.HIDDEN,
            Star = 0,
            CreatedAt = DateTime.Now,
            CreatedBy = fullname,
            UpdatedAt = DateTime.Now,
            IsDeleted = false
        };

        await _unitOfWork.FlashcardRepository.CreateFlashcard(newFlashcard);
        //need implement flashcard content later
        return new ResponseDto(HttpStatusCode.Created, "Subject created successfully.", newFlashcard.Id);
    }

    public async Task<ResponseDto> UpdateFlashcard(FlashcardUpdateDto flashcard)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto> DeleteFlashcard(Guid flashcardId)
    {
        throw new NotImplementedException();
    }
}