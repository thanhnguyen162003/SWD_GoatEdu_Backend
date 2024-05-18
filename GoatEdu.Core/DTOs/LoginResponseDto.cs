using Infrastructure;

namespace GoatEdu.Core.DTOs;

public class LoginResponseDto
{
    public User User { get; set; }
    public string Token { get; set; }
}