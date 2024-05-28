using GoatEdu.Core.DTOs.RoleDto;

namespace GoatEdu.Core.DTOs.AdminDto;

public class UserListDataDto
{
    public ICollection<UserMinimalDto> listUser { get; set; }
    public int UserCount => listUser?.Count ?? 0;
}