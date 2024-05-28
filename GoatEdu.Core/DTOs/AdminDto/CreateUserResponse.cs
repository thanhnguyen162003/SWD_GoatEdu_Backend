namespace GoatEdu.Core.DTOs.AdminDto;

public class CreateUserResponse
{
    public string? Username { get; set; }
    
    public string? Email { get; set; }
    
    public string? FullName { get; set; }
    
    public Guid RoleId { get; set; }
}