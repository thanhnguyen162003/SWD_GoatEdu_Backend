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

    public async Task<IEnumerable<Notification>> GetNotificationsByUserId(Guid? id)
    {
        return await _context.Notifications.Where(x => x.UserId == id).OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<Notification?> GetNotificationByUserId(Guid? userId, Guid notificationId)
    {
        return await _context.Notifications.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == notificationId);
    }

    public async Task<IEnumerable<Notification>> GetNotificationByIds(List<Guid> ids, Guid userId)
    {
        return await _context.Notifications.Where(x => ids.Any(id => id == x.Id) && x.UserId == userId).ToListAsync();
    }

    public async Task<int> CountUnreadNotification(Guid userId)
    {
        return await _context.Notifications.CountAsync(x => x.UserId == userId && x.ReadAt.HasValue);
    }

    public void DeleteAsync(IEnumerable<Notification> listNoti)
    {
        _context.Notifications.RemoveRange(listNoti);
    }
}