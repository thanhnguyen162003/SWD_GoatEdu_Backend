using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Models;
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

    public async Task<List<Notification>> GetNotificationByUserId(Guid id)
    {
        return await _context.Notifications.Where(x => x.UserId == id).OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<List<Notification>> GetNotificationByIds(List<Guid> ids)
    {
        return await _context.Notifications.Where(x => ids.Any(id => id == x.Id)).ToListAsync();
    }

    public void DeleteAsync(List<Notification> listNoti)
    {
        _context.Notifications.RemoveRange(listNoti);
    }
}