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
        var username = AuthenTools.GetCurrentUsername(identity);
        var email = AuthenTools.GetCurrentEmail(identity);
        var fullname = AuthenTools.GetCurrentFullname(identity);
        var role = AuthenTools.GetRole(identity);
        GetCurrentUserId = string.IsNullOrEmpty(extractedId) ? Guid.Empty : new Guid(extractedId);
        GetCurrentUsername = string.IsNullOrEmpty(username) ? "" : username;
        GetCurrentFullname = string.IsNullOrEmpty(fullname) ? "" : fullname;
        GetCurrentEmail = string.IsNullOrEmpty(email) ? "" : email;
        GetRole = string.IsNullOrEmpty(role) ? "" : role;
    }
    public Guid GetCurrentUserId { get; }
    public string GetCurrentUsername { get; }
    public string GetCurrentFullname { get; }
    public string GetCurrentEmail { get; }
    public string GetRole { get; }
}