using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NoteRepository : BaseRepository<Note> ,INoteRepository
{
    private readonly GoatEduContext _context;
    public NoteRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Note>> GetNoteByFilters(NoteQueryFilter queryFilter)
    {
        var notes = _entities.AsQueryable();
        notes = ApplyFilterSortAndSearch(notes, queryFilter);
        notes = ApplySorting(notes, queryFilter);
        return await notes.ToListAsync();
    }

    public async Task<List<Note>> GetNoteByUserId(Guid userId)
    {
        return await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<List<Note>> GetNoteByIds(List<Guid> ids)
    {
        return await _context.Notes.Where(x => ids.Any(id => id == x.Id)).ToListAsync();
    }

    public async Task SoftDelete(List<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }
    
    
    
    private IQueryable<Note> ApplyFilterSortAndSearch(IQueryable<Note> notes, NoteQueryFilter queryFilter)
    {
        notes = notes.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.Search))
        {
            notes = notes.Where(x => x.NoteName.Contains(queryFilter.Search));
        }
        return notes;
    }
    
    private IQueryable<Note> ApplySorting(IQueryable<Note> notes, NoteQueryFilter queryFilter)
    {
        notes = queryFilter.Sort.ToLower() switch
        {
            "name" => queryFilter.SortDirection.ToLower() == "desc"
                ? notes.OrderByDescending(x => x.NoteName)
                : notes.OrderBy(x => x.NoteName),
            _ => queryFilter.SortDirection.ToLower() == "desc"
                ? notes.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.NoteName)
                : notes.OrderBy(x => x.CreatedAt).ThenBy(x => x.NoteName),
        };
        return notes;
    }
}