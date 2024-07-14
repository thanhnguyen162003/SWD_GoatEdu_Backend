using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services.SignalR;

public class MyIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
    }
}