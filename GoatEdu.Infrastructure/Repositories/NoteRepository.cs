using GoatEdu.Core.Interfaces.NoteInterfaces;
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
}