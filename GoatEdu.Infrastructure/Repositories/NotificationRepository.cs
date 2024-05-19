using GoatEdu.Core.Interfaces.NotificationInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    private readonly GoatEduContext _context;
    
    public NotificationRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }


    public async Task<List<Notification>> GetNotificationByUserId(Guid id)
    {
        return await _context.Notifications.Where(x => x.UserId == id).ToListAsync();
    }

    public async Task<List<Notification>> GetNotificationByIds(List<Guid> ids)
    {
        return await _context.Notifications.Where(x => ids.Any(id => id == x.Id)).ToListAsync();
    }
}