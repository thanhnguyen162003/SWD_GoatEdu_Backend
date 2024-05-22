using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
{
    private readonly GoatEduContext _context;

    public LessonRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
}