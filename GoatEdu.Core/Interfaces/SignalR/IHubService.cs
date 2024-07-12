using GoatEdu.Core.DTOs.NotificationDto;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Interfaces.SignalR;

public interface IHubService
{
    Task Voted(string mess, int votes);
    Task SendNotification(string mess);
    Task SendAnswer(object eventData);
    Task SendAsync(string mess);
}