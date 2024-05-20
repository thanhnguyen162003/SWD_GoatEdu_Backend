using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NotificationInterfaces;

public interface INotificationService
{
    Task<ResponseDto> GetNotificationById(Guid id);
    Task<PagedList<NotificationResponseDto>> GetNotificationByUserId(NotificationQueryFilter queryFilter);
    Task<ResponseDto> InsertNotification(List<NotificationRequestDto> notification);
    Task<ResponseDto> DeleteNotification(List<Guid> ids);
}