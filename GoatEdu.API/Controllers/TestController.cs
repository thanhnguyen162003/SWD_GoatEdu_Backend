using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Services.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[ApiController]
[Route("/api/signalr")]
public class TestController : ControllerBase
{
    private readonly IHubContext<MyHub> _hubContext;
    private readonly IClaimsService _claimsService;
    private readonly IUserService _userService;

    public TestController(IHubContext<MyHub> hubContext, IClaimsService claimsService, IUserService userService)
    {
        _hubContext = hubContext;
        _claimsService = claimsService;
        _userService = userService;
    }

    // [HttpGet]
    // // [Authorize]
    // public async Task TestSignalR()
    // {
    //     var userId = _claimsService.GetCurrentUserId;
    //     var user = await _userService.GetUserByUsername("ditmekhang1");
    //     var json = JsonConvert.SerializeObject(user);
    //     await _hubContext.Clients.All.SendAsync("VoteForMe" , json);
    //     // await _hubContext.Clients.All.SendAsync("VoteForMe");
    // }
}