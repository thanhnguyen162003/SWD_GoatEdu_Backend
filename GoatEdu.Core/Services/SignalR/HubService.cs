using System.Net;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.Interfaces.VoteInterface;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services.SignalR;
// <IHubService>
public class HubService : Hub
{
    private readonly IVoteService _voteService;
    private readonly IUnitOfWork _unitOfWork;

    public HubService(IVoteService voteService, IUnitOfWork unitOfWork)
    {
        _voteService = voteService;
        _unitOfWork = unitOfWork;
    }

    public async Task SendVoteAnswer(Guid userId, Guid answerId)
    {
        var result = await _voteService.AnswerVoting(userId, answerId);
        var votes = await _unitOfWork.VoteRepository.GetVotesNumber(answerId, "answer");
        await Clients.All.SendAsync("Voted", result.Message, votes);
    }
    
    public async Task SendVoteDiscussion(Guid userId, Guid discussionId)
    {
        var result = await _voteService.DiscussionVoting(userId, discussionId);
        var votes = await _unitOfWork.VoteRepository.GetVotesNumber(discussionId, "discussion");
        await Clients.All.SendAsync("Voted", result.Message, votes);
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