using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetNotificationByUserId(Guid id);
    Task<List<Notification>> GetNotificationByIds(List<Guid> ids);
    
}