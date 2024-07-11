using GoatEdu.Core.DTOs.NotificationDto;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Interfaces.SignalR;

public interface IHubService
{
    Task Voted(string method, string mess, int votes);
    Task SendNotification(string method, object eventData);
    Task SendAnswer(object eventData);
    Task SendVoteEvent(string method, string mess);
    Task SendAsync(string mess);
}