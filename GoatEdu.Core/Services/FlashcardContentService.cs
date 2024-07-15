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
using GoatEdu.Core.Interfaces.GenericInterfaces;
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
    private readonly ICurrentTime _currentTime;

    public FlashcardContentService(IUnitOfWork unitOfWork, IMapper mapper,
        IOptions<PaginationOptions> paginationOptions, IClaimsService claimsService, ICurrentTime currentTime)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
        _claimsService = claimsService;
        _currentTime = currentTime;
    }

    public async Task<IEnumerable<FlashcardContentDto>> GetFlashcards(FlashcardQueryFilter queryFilter,
        Guid flashcardId)
    {
        queryFilter.page_number =
            queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? 100 : queryFilter.page_size;

        var listFlashcard = await _unitOfWork.FlashcardContentRepository.GetFlashcardContent(queryFilter, flashcardId);

        if (!listFlashcard.Any())
        {
            return new PagedList<FlashcardContentDto>(new List<FlashcardContentDto>(), 0, 0, 0);
        }

        var mapperList = _mapper.Map<List<FlashcardContentDto>>(listFlashcard);
        return PagedList<FlashcardContentDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> CreateFlashcardContent(List<FlashcardContentDto> listFlashcardContent,
        Guid flashcardId)
    {
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

        var fullname = _claimsService.GetCurrentFullname;
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

    public async Task<ResponseDto> UpdateFlashcardContent(FlashcardContentDto flashcard, Guid id)
    {
        var userId = _claimsService.GetCurrentUserId;
        var mapper = _mapper.Map<FlashcardContent>(flashcard);
        mapper.Id = id;
        return await _unitOfWork.FlashcardContentRepository.UpdateFlashcardContent(mapper, userId);
    }

    public async Task<ResponseDto> UpdateFlashcardContents(Guid flashcardId, IEnumerable<FlashcardContentDto> flashcard)
    {
        var userId = _claimsService.GetCurrentUserId;

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var flashcardAdd = flashcard.Where(x => x.id == Guid.Empty).ToList();
            if (flashcardAdd.Any())
            {
                foreach (var data in flashcardAdd)
                {
                    data.id = null;
                }

                var addresult = await CreateFlashcardContent(flashcardAdd, flashcardId);
                if (addresult.Status != HttpStatusCode.OK)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    addresult.Message = "Failed at Create FlashcardContent";
                    return addresult;
                }
            }
            
            var flashcardUpdate = flashcard.Where(x => x.id != null);

            var ids = flashcardUpdate.Select(x => x.id);
            var flashcardContents = await _unitOfWork.FlashcardContentRepository.GetFlashcardContentByIds(userId, ids);
            if (!flashcardContents.Any())
            {
                return new ResponseDto(HttpStatusCode.NotFound, "You dont own this flashcard");
            }

            foreach (var flashcardContent in flashcardContents)
            {
                var dto = flashcardUpdate.FirstOrDefault(x => x.id == flashcardContent.Id);
                flashcardContent.FlashcardContentQuestion = dto.flashcardContentQuestion;
                flashcardContent.FlashcardContentAnswer = dto.flashcardContentAnswer;
                flashcardContent.UpdatedAt = _currentTime.GetCurrentTime();
                flashcardContent.UpdatedBy = _claimsService.GetCurrentFullname;
            }

            _unitOfWork.FlashcardContentRepository.UpdateRange(flashcardContents);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseDto(HttpStatusCode.BadGateway, "Update Failed!");
            }

            await _unitOfWork.CommitTransactionAsync();
            return new ResponseDto(HttpStatusCode.OK, "Update Successfully!");
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ResponseDto(HttpStatusCode.InternalServerError, "Server Error");
        }
    }

    public async Task<ResponseDto> DeleteFlashcardContent(Guid flashcardContentId)
    {
        var userId = _claimsService.GetCurrentUserId;
        return await _unitOfWork.FlashcardContentRepository.DeleteFlashcardContent(flashcardContentId, userId);
    }
}