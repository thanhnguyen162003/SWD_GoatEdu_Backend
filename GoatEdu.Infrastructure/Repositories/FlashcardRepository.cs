using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Enumerations;
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

    public async Task DisableUnder4TagsFlashcard()
    { 
        await _entities.Where(x => x.Tags.Count(tag => tag.IsDeleted == false) < 4 && x.IsDeleted == false).ForEachAsync(x => x.Status = StatusConstraint.VAC);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Flashcard>> GetFlashcards(FlashcardQueryFilter queryFilter)
    {
        var flashcards = _entities.Include(x=>x.User)
            .Include(y=> y.Subject)
            .Include(y=> y.FlashcardContents)
            .AsQueryable();
        flashcards = ApplyFilterSortAndSearch(flashcards, queryFilter);
        flashcards = ApplySorting(flashcards, queryFilter);
        return await flashcards.ToListAsync();
    }
    public async Task<IEnumerable<Flashcard>> GetOwnFlashcard(FlashcardQueryFilter queryFilter, Guid userId)
    {
        var flashcards = _entities.AsNoTracking().Where(x=>x.UserId == userId)
            .Include(x=>x.User)
            .Include(y=> y.Subject)
            .Include(y=> y.FlashcardContents)
            .AsQueryable();
        flashcards = ApplyFilterSortAndSearchOwn(flashcards, queryFilter);
        flashcards = ApplySorting(flashcards, queryFilter);
        return await flashcards.ToListAsync();
    }

    public async Task<Flashcard> GetFlashcardById(Guid flashcardId)
    {
        return await _entities.AsNoTracking().Where(x => x.Id == flashcardId).Include(x=>x.Subject).FirstOrDefaultAsync();
    }
    
    public async Task<Flashcard> GetFlashcarDetail(Guid flashcardId)
    {
        return await _entities.AsNoTracking().Include(x=>x.User).Where(x => x.Id == flashcardId).Include(x=>x.Subject).FirstOrDefaultAsync();
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
        flashcards = flashcards.Where(x => x.IsDeleted == false && x.Status.Equals(StatusConstraint.OPEN));
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            flashcards = flashcards.Where(x => x.FlashcardName.Contains(queryFilter.search));
        }
        return flashcards;
    }
    private IQueryable<Flashcard> ApplyFilterSortAndSearchOwn(IQueryable<Flashcard> flashcards, FlashcardQueryFilter queryFilter)
    {
        flashcards = flashcards.Where(x => x.IsDeleted == false && (x.Status == StatusConstraint.OPEN || x.Status == StatusConstraint.HIDDEN));
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
                ? flashcards.OrderByDescending(x => x.Star)
                : flashcards.OrderBy(x => x.Star),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? flashcards.OrderByDescending(x => x.Star).ThenBy(x => x.Star)
                : flashcards.OrderBy(x => x.Star).ThenBy(x => x.Star),
        };
        return flashcards;
    }
}