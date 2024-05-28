using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Security;

public interface JWTGenerator
{
    Task<string> GenerateToken(LoginDtoRequest user);
    Task<string> GenerateTokenGoogle(string email);
}