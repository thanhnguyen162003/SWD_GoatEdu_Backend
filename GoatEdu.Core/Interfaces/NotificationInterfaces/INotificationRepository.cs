using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Models;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id);
    Task AddAsync(Notification entitie);
    Task<List<Notification>> GetNotificationByUserId(Guid userId);
    Task<List<Notification>> GetNotificationByIds(List<Guid> ids);
    void DeleteAsync(List<Notification> listNoti);
}