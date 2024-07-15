using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id);
    Task AddAsync(Notification entity);
    Task<IEnumerable<Notification>> GetNotificationsByUserId(Guid? userId);
    Task<Notification?> GetNotificationByUserId(Guid? userId, Guid notificationId);
    Task<IEnumerable<Notification>> GetNotificationByIds(List<Guid> ids, Guid userId);
    Task<int> CountUnreadNotification(Guid userId);
    void DeleteAsync(IEnumerable<Notification> listNoti);
}