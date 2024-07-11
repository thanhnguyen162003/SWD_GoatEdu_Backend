// using GoatEdu.Core.Interfaces.SignalR;
// using GoatEdu.Core.Services.SignalR;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.SignalR;
//
// namespace GoatEdu.API.Controllers;
//
// [ApiController]
// [Route("/api/signalr")]
// public class TestController : ControllerBase
// {
//     private readonly IHubContext<HubService, IHubService> _hubContext;
//
//     public TestController(IHubContext<HubService, IHubService> hubContext)
//     {
//         _hubContext = hubContext;
//     }
//
//     [HttpGet]
//     public async Task TestSignalR()
//     {
//         await _hubContext.Clients.All.SendVoteAnswer("Voted", "vote ok");
//     }
// }