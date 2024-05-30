using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id);
    Task AddAsync(Notification entitie);
    Task<IEnumerable<Notification>> GetNotificationByUserId(Guid? userId);
    Task<IEnumerable<Notification>> GetNotificationByIds(List<Guid> ids);
    void DeleteAsync(IEnumerable<Notification> listNoti);
}