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
    private readonly IHubContext<HubService, IHubService> _hubContext;
    private readonly IClaimsService _claimsService;

    public TestController(IHubContext<HubService, IHubService> hubContext, IClaimsService claimsService)
    {
        _hubContext = hubContext;
        _claimsService = claimsService;
    }

    [HttpGet]
    [Authorize]
    public async Task TestSignalR()
    {
        var userId = _claimsService.GetCurrentUserId.ToString();
        await _hubContext.Clients.Client(userId).SendAsync("vote ok");
    }
}