using GoatEdu.Core.Interfaces.TranstractionInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubscriptionRepository : BaseRepository<Subscription>, ISubcriptionRepository
{
    private readonly GoatEduContext _context;

    public SubscriptionRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Subscription> GetSubscriptionById(Guid subscriptionId)
    {
        return await _entities.Where(x => x.Id == subscriptionId).FirstOrDefaultAsync();
    }
}