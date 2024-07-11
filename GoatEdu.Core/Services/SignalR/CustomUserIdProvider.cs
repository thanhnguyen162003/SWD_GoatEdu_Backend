using GoatEdu.Core.Interfaces.ClaimInterfaces;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services.SignalR;

public class CustomUserIdProvider : IUserIdProvider
{

    public string? GetUserId(HubConnectionContext connection)
    {
        var userId = connection.User.FindFirst("UserId")?.Value!;
        return userId;
    }
}