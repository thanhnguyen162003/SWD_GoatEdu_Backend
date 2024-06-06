using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly GoatEduContext _context;
    
    public NotificationRepository(GoatEduContext context)
    {
        _context = context;
    }

    public async Task<Notification?> GetByIdAsync(Guid id)
    {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task AddAsync(Notification entitie)
    {
        await _context.Notifications.AddAsync(entitie);
    }

    public async Task<IEnumerable<Notification>> GetNotificationByUserId(Guid? id)
    {
        return await _context.Notifications.AsNoTracking().Where(x => x.UserId == id).OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Notification>> GetNotificationByIds(List<Guid> ids)
    {
        return await _context.Notifications.Where(x => ids.Any(id => id == x.Id)).ToListAsync();
    }

    public void DeleteAsync(IEnumerable<Notification> listNoti)
    {
        _context.Notifications.RemoveRange(listNoti);
    }
}