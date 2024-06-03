using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class FlashcardContentRepository : BaseRepository<FlashcardContent>, IFlashcardContentRepository
{
    private readonly GoatEduContext _context;

    public FlashcardContentRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
}