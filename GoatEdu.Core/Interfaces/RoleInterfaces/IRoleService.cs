using GoatEdu.Core.DTOs.RoleDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.RoleInterfaces;

public interface IRoleService
{
    Task<ICollection<RoleDto>> GetAllRole();
    Task<RoleDto> GetRoleByRoleId(Guid id);
    Task<RoleDto> GetRoleByRoleName(string roleName);
    Task<UserRoleDto> GetUsersInRole(Guid roleId);
}