using GoatEdu.Core.Interfaces.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services.SignalR;

public class HubService : Hub<IHubService>
{
    public async Task SendNotification(object eventData)
    {
        await Clients.All.SendNotification(new { Type = "Notification", eventData });
    }

    // public async Task SendVote(string username)
    // {
    //     await Clients.All.SendVote(new { Type = "Vote", Username = username });
    // }
    
    public async Task SendAnswer(object eventData)
    {
        await Clients.All.SendAnswer(new { Type = "Answer", eventData });
    }
}