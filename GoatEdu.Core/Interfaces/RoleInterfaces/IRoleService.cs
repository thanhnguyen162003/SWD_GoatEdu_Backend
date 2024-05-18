using GoatEdu.Core.DTOs.RoleDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.RoleInterfaces;

public interface IRoleService
{
    Task<ICollection<RoleResponseDto>> GetAllRole();
    Task<RoleResponseDto> GetRoleByRoleId(Guid id);
    Task<RoleResponseDto> GetRoleByRoleName(string roleName);
}