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
    public async Task<ResponseDto> UpdateFlashcardContent(FlashcardContent flashcard, Guid id)
    {
        var flashcardUpdate = await _entities.FirstOrDefaultAsync(x => x.Flashcard.UserId == id && x.Id == flashcard.Id);

        if (flashcardUpdate == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Flashcard does not exist, maybe you need to own this flashcard");
        }
        flashcardUpdate.FlashcardContentAnswer = flashcard.FlashcardContentAnswer ?? flashcardUpdate.FlashcardContentAnswer;
        flashcardUpdate.FlashcardContentQuestion = flashcard.FlashcardContentQuestion ?? flashcardUpdate.FlashcardContentQuestion;
        flashcardUpdate.Image = flashcard.Image ?? flashcardUpdate.Image;
        flashcardUpdate.Status = flashcard.Status ?? flashcardUpdate.Status;
        flashcardUpdate.UpdatedAt = DateTime.Now;

        _entities.Update(flashcardUpdate);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Flashcard Content successfully updated.");
    }
    
    public async Task<ResponseDto> DeleteFlashcardContent(Guid flashcardContentId, Guid userId)
    {
        var flashcards = await _entities.Where(x => x.Id == flashcardContentId && x.Flashcard.UserId == userId).FirstOrDefaultAsync();
        if (flashcards is null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Cant get any flashcard content, make sure you own this flashcard and check again");
        }
        // Set IsDeleted to true
        flashcards.IsDeleted = true;
        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return a successful response
        return new ResponseDto(HttpStatusCode.OK, "Flashcard Content successfully deleted.");
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