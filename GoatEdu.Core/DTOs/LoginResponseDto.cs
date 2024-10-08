using GoatEdu.Core.DTOs.RoleDto;
using Infrastructure;

namespace GoatEdu.Core.DTOs;

public class LoginResponseDto
{
    public Guid userId { get; set; }
    
    public string username { get; set; }

    public string fullname { get; set; }

    public string email { get; set; }
    
    public string image { get; set; }
    public string phoneNumber { get; set; }

    
    public bool emailVerify { get; set; }
    
    public RoleDto.RoleDto Role { get; set; }

    public string Token { get; set; }
    
    public bool subscription { get; set; }
    
    public bool isNewUser { get; set; }

}