using GoatEdu.Core.DTOs.RoleDto;


namespace GoatEdu.API.Response;

public class LoginResponseModel
{
    public Guid userId { get; set; }
    
    public string username { get; set; }

    public string fullname { get; set; }

    public string email { get; set; }
    
    public string image { get; set; }
    
    public bool emailVerify { get; set; }
    
    public RoleResponseModel Role { get; set; }

    public string Token { get; set; }
}