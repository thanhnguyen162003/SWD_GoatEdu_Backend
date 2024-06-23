using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.TheoryFlashcardContentInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TheoryFlashcardContentRepository : BaseRepository<TheoryFlashCardContent>, ITheoryFlashcardContentRepository
{
    public TheoryFlashcardContentRepository(GoatEduContext context) : base(context)
    {
    }

    public async Task<List<TheoryFlashCardContent>> GetTheoryFlashCardContentByIds(Guid theoryId, IEnumerable<Guid?> guids)
    {
        return await _entities.Where(x => guids.Contains(x.Id) && x.TheoryId == theoryId).ToListAsync();
    }
    
    public async Task SoftDelete(IEnumerable<Guid> guids)
    {
        await _entities.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.IsDeleted = true);
    }

    public async Task<IEnumerable<TheoryFlashCardContent>> GetTheoryFlashCardContentByTheory(Guid theoryId, string role)
    {
        var query = _entities.AsNoTracking().AsQueryable();
        
        query = query.Where(x => x.TheoryId == theoryId);
        query = query.Where(x => x.IsDeleted == false);
        
        if (role.Equals(UserEnum.MODERATOR))
        {
            query = query.Where(x =>
                x.Status.Equals(StatusConstraint.OPEN) || x.Status.Equals(StatusConstraint.HIDDEN));
        }
        else
        {
            query = query.Where(x => x.Status.Equals(StatusConstraint.OPEN));
        }

        query = query.OrderBy(x => x.CreatedAt);
        
        return await query.ToListAsync();
    }
}