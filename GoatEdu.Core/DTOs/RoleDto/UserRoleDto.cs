namespace GoatEdu.Core.DTOs.RoleDto;

public class UserRoleDto
{
    public string? RoleName { get; set; }
    public ICollection<UserMinimalDto> UserMinimalDtos { get; set; }
    public int UserCount => UserMinimalDtos?.Count ?? 0; // Computed property for user count
}
