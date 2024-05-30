using GoatEdu.Core.DTOs.NotificationDto;

namespace GoatEdu.Core.Interfaces.SignalR;

public interface INotificationHub
{
    public Task SendMessage(NotificationResponseDto dto);
}