using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.Services.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.API.Controllers;

[ApiController]
[Route("/api/signalr")]
public class TestController : ControllerBase
{
    private readonly IHubContext<MyHub> _hubContext;
    private readonly IClaimsService _claimsService;

    public TestController(IHubContext<MyHub> hubContext, IClaimsService claimsService)
    {
        _hubContext = hubContext;
        _claimsService = claimsService;
    }

    [HttpGet]
    [Authorize]
    public async Task TestSignalR()
    {
        var userId = _claimsService.GetCurrentUserId;
        await _hubContext.Clients.User(userId.ToString()).SendAsync("VoteForMe" ,"OK BRO");
        // await _hubContext.Clients.All.SendAsync("VoteForMe");
    }
}