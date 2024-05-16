using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.Security;

public interface JWTGenerator
{
    string GenerateToken(LoginDtoRequest user);
}