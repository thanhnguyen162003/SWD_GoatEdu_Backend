using GoatEdu.Core.DTOs.NotificationDto;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Interfaces.SignalR;

public interface IHubService
{
    Task SendNotification(object eventData);
    Task SendVote(object eventData);

}