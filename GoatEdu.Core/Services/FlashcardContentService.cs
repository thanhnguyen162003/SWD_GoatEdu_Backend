using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class FlashcardContentService : IFlashcardContentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly IClaimsService _claimsService;

    public FlashcardContentService(IUnitOfWork unitOfWork,IMapper mapper, IOptions<PaginationOptions> paginationOptions,IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
        _claimsService = claimsService;
    }
    public async Task<IEnumerable<FlashcardContentDto>> GetFlashcards(FlashcardQueryFilter queryFilter, Guid flashcardId)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? 100 : queryFilter.page_size;
        
        var listFlashcard = await _unitOfWork.FlashcardContentRepository.GetFlashcardContent(queryFilter, flashcardId);
        
        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardContentDto>(new List<FlashcardContentDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<FlashcardContentDto>>(listFlashcard);
        return PagedList<FlashcardContentDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }
    
    public async Task<ResponseDto> CreateFlashcardContent(List<FlashcardContentDto> listFlashcardContent, Guid flashcardId)
    {
        var fullname = _claimsService.GetCurrentFullname;
        var userId = _claimsService.GetCurrentUserId;
        var flashcard = await _unitOfWork.FlashcardRepository.GetFlashcardById(flashcardId);
        if (flashcard == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Flashcard not found.");
        }
        if (flashcard.UserId != userId)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "This flashcard not own by you !!!.");
        }

        var newFlashcardContents = listFlashcardContent.Select(contentDto => new FlashcardContent
        {
            FlashcardContentQuestion = contentDto.flashcardContentQuestion,
            FlashcardContentAnswer = contentDto.flashcardContentAnswer,
            CreatedBy = fullname,
            Status = StatusConstraint.OPEN,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            FlashcardId = flashcardId
        }).ToList();

        return await _unitOfWork.FlashcardContentRepository.CreateFlashcardContent(newFlashcardContents);
    }
}