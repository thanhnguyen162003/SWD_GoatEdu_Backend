using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlashcardContentRepository : BaseRepository<FlashcardContent>, IFlashcardContentRepository
{
    private readonly GoatEduContext _context;

    public FlashcardContentRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<FlashcardContent>> GetFlashcardContent(FlashcardQueryFilter queryFilter, Guid flashcardId)
    {
        var flashcardContents = _entities.AsNoTracking().Where(x =>x.FlashcardId ==flashcardId).AsQueryable();
        flashcardContents = ApplyFilterSortAndSearch(flashcardContents, queryFilter);
        flashcardContents = ApplySorting(flashcardContents, queryFilter);
        return await flashcardContents.ToListAsync();
    }

    public async Task<ResponseDto> CreateFlashcardContent(List<FlashcardContent> flashcardContents)
    {
        try
        {
            await _context.FlashcardContents.AddRangeAsync(flashcardContents);
            await _context.SaveChangesAsync();
            return new ResponseDto(HttpStatusCode.OK, "Create FlashcardContentSuccess");
        }
        catch
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Create FlashcardContentFail");
        }
        
    }

    private IQueryable<FlashcardContent> ApplyFilterSortAndSearch(IQueryable<FlashcardContent> flashcards, FlashcardQueryFilter queryFilter)
    {
        flashcards = flashcards.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            flashcards = flashcards.Where(x => x.FlashcardContentQuestion.Contains(queryFilter.search));
        }
        return flashcards;
    }

   
    
    private IQueryable<FlashcardContent> ApplySorting(IQueryable<FlashcardContent> flashcards, FlashcardQueryFilter queryFilter)
    {
        flashcards = queryFilter.sort.ToLower() switch
        {
            "name" => queryFilter.sort_direction.ToLower() == "desc"
                ? flashcards.OrderByDescending(x => x.CreatedAt)
                : flashcards.OrderBy(x => x.CreatedAt),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? flashcards.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.CreatedAt)
                : flashcards.OrderBy(x => x.CreatedAt).ThenBy(x => x.CreatedAt),
        };
        return flashcards;
    }
}