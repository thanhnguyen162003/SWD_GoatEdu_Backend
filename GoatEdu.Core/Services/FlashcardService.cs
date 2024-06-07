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
    
    public async Task<IEnumerable<FlashcardDto>> GetFlashcards(FlashcardQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listFlashcard = await _unitOfWork.FlashcardRepository.GetFlashcards(queryFilter);
        
        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardDto>(new List<FlashcardDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<FlashcardDto>>(listFlashcard);
        return PagedList<FlashcardDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<IEnumerable<FlashcardDto>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid subjectId)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listFlashcard = await _unitOfWork.FlashcardRepository.GetFlashcardsBySubject(queryFilter,subjectId);
        
        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardDto>(new List<FlashcardDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<FlashcardDto>>(listFlashcard);
        return PagedList<FlashcardDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> CreateFlashcard(FlashcardDto flashcard, Guid subjectId)
    {
        var userId = _claimsService.GetCurrentUserId;
        var fullname = _claimsService.GetCurrentFullname;

        var newFlashcard = new Flashcard()
        {
            FlashcardName = flashcard.flashcardName,
            FlashcardDescription = flashcard.flashcardDescription,
            UserId = userId,
            SubjectId = subjectId,
            Status = StatusConstraint.OPEN,
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
    
    public async Task<FlashcardDto> GetFlashcarDetail(Guid flashcardId)
    {
        var listFlashcard = await _unitOfWork.FlashcardRepository.GetFlashcarDetail(flashcardId);
        var mapper = _mapper.Map<FlashcardDto>(listFlashcard);
        return mapper;
    }

    public async Task<IEnumerable<FlashcardDto>> GetOwnFlashcard(FlashcardQueryFilter queryFilter)
    {
        var userId = _claimsService.GetCurrentUserId;
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        
        var listFlashcard = await _unitOfWork.FlashcardRepository.GetOwnFlashcard(queryFilter,userId);
        
        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardDto>(new List<FlashcardDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<FlashcardDto>>(listFlashcard);
        return PagedList<FlashcardDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> UpdateFlashcard(FlashcardDto flashcard, Guid id)
    {
        var userId = _claimsService.GetCurrentUserId;
        var mapper = _mapper.Map<Flashcard>(flashcard);
        mapper.Id = id;
        return await _unitOfWork.FlashcardRepository.UpdateFlashcard(mapper, userId);
    }

    public async Task<ResponseDto> DeleteFlashcard(Guid flashcardId)
    {
        var userId = _claimsService.GetCurrentUserId;
        return await _unitOfWork.FlashcardRepository.DeleteFlashcard(flashcardId, userId);
    }
}