namespace GoatEdu.Core.DTOs.RoleDto;

public class UserMinimalDto
{
    public Guid? Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Fullname { get; set; }
    public string? RoleName { get; set; }
    public bool? IsConfirmMail { get; set; }
    public bool? IsDeleted { get; set; }



}