using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlashcardRepository : BaseRepository<Flashcard>, IFlashcardRepository
{
    private readonly GoatEduContext _context;
    
    public FlashcardRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Flashcard>> GetTwoTagFlashcard()
    {
        return await _entities.Where(x => x.Tags.Count(tag => tag.IsDeleted == false) <= 2).ToListAsync();
    }

    public async Task<IEnumerable<Flashcard>> GetFlashcards(FlashcardQueryFilter queryFilter)
    {
        var flashcards = _entities.Include(x=>x.User).AsQueryable();
        flashcards = ApplyFilterSortAndSearch(flashcards, queryFilter);
        flashcards = ApplySorting(flashcards, queryFilter);
        return await flashcards.ToListAsync();
    }

    public async Task<Flashcard> GetFlashcardById(Guid flashcardId)
    {
        return await _entities.AsNoTracking().Where(x => x.Id == flashcardId).FirstOrDefaultAsync();
    }
    
    public async Task<Flashcard> GetFlashcarDetail(Guid flashcardId)
    {
        return await _entities.AsNoTracking().Include(x=>x.User).Where(x => x.Id == flashcardId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Flashcard>> GetFlashcardsBySubject(FlashcardQueryFilter queryFilter, Guid id)
    {
        var flashcards = _entities.Where(x=>x.SubjectId == id).Include(x=>x.User).AsQueryable();
        flashcards = ApplyFilterSortAndSearch(flashcards, queryFilter);
        flashcards = ApplySorting(flashcards, queryFilter);
        return await flashcards.ToListAsync();
    }

    public async Task<ResponseDto> CreateFlashcard(Flashcard flashcard)
    {
        _context.Flashcards.Add(flashcard);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Create flashcard success");
    }

    public async Task<ResponseDto> UpdateFlashcard(Flashcard flashcard, Guid id)
    {
        var flashcardUpdate = await _entities.FirstOrDefaultAsync(x => x.Id == flashcard.Id && x.UserId == id);

        if (flashcardUpdate == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Flashcard does not exist, maybe you need to own this flashcard");
        }
        flashcardUpdate.FlashcardName = flashcard.FlashcardName ?? flashcardUpdate.FlashcardName;
        flashcardUpdate.FlashcardDescription = flashcard.FlashcardDescription ?? flashcardUpdate.FlashcardDescription;
        flashcardUpdate.UpdatedAt = DateTime.Now;

        _entities.Update(flashcardUpdate);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Flashcard successfully updated.");
    }

    public async Task<ResponseDto> DeleteFlashcard(Guid flashcardId, Guid userId)
    {
        var flashcards = await _entities.Where(x => x.Id == flashcardId && x.UserId == userId).FirstOrDefaultAsync();
        if (flashcards is null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Cant get any flashcard, make sure you own flashcard");
        }
        // Set IsDeleted to true
        flashcards.IsDeleted = true;
        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return a successful response
        return new ResponseDto(HttpStatusCode.OK, "Flashcard successfully deleted.");
    }
    
    private IQueryable<Flashcard> ApplyFilterSortAndSearch(IQueryable<Flashcard> flashcards, FlashcardQueryFilter queryFilter)
    {
        flashcards = flashcards.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            flashcards = flashcards.Where(x => x.FlashcardName.Contains(queryFilter.search));
        }
        return flashcards;
    }
    
    private IQueryable<Flashcard> ApplySorting(IQueryable<Flashcard> flashcards, FlashcardQueryFilter queryFilter)
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