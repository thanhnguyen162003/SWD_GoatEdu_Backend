using System.Security.Claims;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.Security;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.Services;

public class ClaimsService : IClaimsService
{
    public ClaimsService(IHttpContextAccessor httpContextAccessor)
    {
        // todo implementation to get the current userId
        var identity = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
        var extractedId = AuthenTools.GetCurrentAccountId(identity);
        GetCurrentUserId = string.IsNullOrEmpty(extractedId) ? "" : extractedId;
    }
    public string GetCurrentUserId { get; }
}