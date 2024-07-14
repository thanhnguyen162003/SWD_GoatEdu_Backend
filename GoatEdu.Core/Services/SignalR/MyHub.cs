using System.Net;
using System.Security.Claims;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.Interfaces.VoteInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services.SignalR;
// <IHubService>
public class MyHub : Hub
{
    private readonly IVoteService _voteService;
    private readonly IUnitOfWork _unitOfWork;

    public MyHub(IVoteService voteService, IUnitOfWork unitOfWork)
    {
        _voteService = voteService;
        _unitOfWork = unitOfWork;
    }
    
    [Authorize]
    public async Task SendVoteAnswer(Guid answerId)
    {
        var userClaims = Context.User?.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userId = Guid.Parse(userClaims);
        var result = await _voteService.AnswerVoting(userId, answerId);
        var votes = await _unitOfWork.VoteRepository.GetVotesNumber(answerId, "answer");
        await Clients.All.SendAsync("VoteAnswer", result.Message, votes);
    }
    
    [Authorize]
    public async Task SendVoteDiscussion(Guid discussionId)
    {
        var userClaims = Context.User?.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userId = Guid.Parse(userClaims);
        var result = await _voteService.DiscussionVoting(userId, discussionId);
        var votes = await _unitOfWork.VoteRepository.GetVotesNumber(discussionId, "discussion");
        await Clients.All.SendAsync("Voted", result.Message, votes);
    }
    
    // [Authorize(Roles = UserEnum.GOOGLE)]
    // [Authorize]
    // public async Task SendNotificationTo(string mess)
    // {
    //     var userId = Context.User?.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
    //     if (!string.IsNullOrEmpty(userId))
    //     {
    //         await Clients.User(userId).SendNotification("You have new notification!");
    //     }
    // }
    
    // public async Task SendAnswer(object eventData)
    // {
    //     await Clients.All.SendAnswer(new { Type = "Answer", eventData });
    // }
    
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiverMessage: " +  $"{Context.ConnectionId} has joined");
        await base.OnConnectedAsync();
    }
}