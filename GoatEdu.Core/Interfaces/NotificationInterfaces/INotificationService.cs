using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationService
{
    Task<ResponseDto> GetNotificationById(Guid id);
    Task<ResponseDto> GetNotificationByUserId(Guid id);
    Task<ResponseDto> InsertNotification(NotificationRequestDto notification);
    Task<ResponseDto> DeleteNotification(List<Guid> ids);
}