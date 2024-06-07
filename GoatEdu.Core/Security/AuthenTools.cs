using System.Security.Claims;

namespace GoatEdu.Core.Interfaces.Security;

public class AuthenTools
{
    public static string GetCurrentAccountId(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
        return null;
    }
    
    public static string GetCurrentUsernam(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
        return null;
    }
    
    public static string GetCurrentFullname(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault(x => x.Type == "Fullname")?.Value;
        }
        return null;
    }
}