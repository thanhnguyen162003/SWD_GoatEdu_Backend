using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ChapterRepository : BaseRepository<Chapter>, IChapterRepository
{
    private readonly GoatEduContext _context;

    public ChapterRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
}