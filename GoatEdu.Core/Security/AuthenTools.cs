using System.Security.Claims;

namespace GoatEdu.Core.Interfaces.Security;

public class AuthenTools
{
    public static string GetCurrentAccountId(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
        }
        return null;
    }
}