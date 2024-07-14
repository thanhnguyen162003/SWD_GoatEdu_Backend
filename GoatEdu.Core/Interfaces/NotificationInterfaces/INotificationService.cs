using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationService
{
    Task<ResponseDto> MarkReadAllNotifications(Guid userId);
    Task<PagedList<NotificationDto>> GetNotificationByCurrentUser(NotificationQueryFilter queryFilter);
    Task<ResponseDto> InsertNotification(NotificationDto notification);
    Task<ResponseDto> DeleteNotifications(List<Guid> ids);
    Task SendNotification(Guid userId);
}