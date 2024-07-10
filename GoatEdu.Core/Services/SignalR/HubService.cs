using System.Net;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.Interfaces.VoteInterface;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services.SignalR;
// <IHubService>
public class HubService : Hub
{
    private readonly IVoteService _voteService;

    public HubService(IVoteService voteService)
    {
        _voteService = voteService;
    }

    public async Task SendVoteAnswer(Guid userId, Guid answerId)
    {
        var result = await _voteService.AnswerVoting(userId, answerId);
        await Clients.All.SendAsync("Voted", result.Message);
    }
    
    public async Task SendVoteDiscussion(Guid userId, Guid discussionId)
    {
        var result = await _voteService.DiscussionVoting(userId, discussionId);
        await Clients.All.SendAsync("Voted", result.Message);
    }
    
    // public async Task SendNotification(object eventData)
    // {
    //     // await Clients.All.SendNotification(new { Type = "Notification", eventData });
    //     await Clients.All.Se
    // }
    
    // public async Task SendAnswer(object eventData)
    // {
    //     await Clients.All.SendAnswer(new { Type = "Answer", eventData });
    // }
    
    // public override async Task OnConnectedAsync()
    // {
    //     await Clients.All.SendAsync("ReceiverMessage" +  $"{Context.ConnectionId} has joined");
    //     await base.OnConnectedAsync();
    // }
}