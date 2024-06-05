using GoatEdu.Core.DTOs.RoleDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.RoleInterfaces;

public interface IRoleRepository
{
    Task<ICollection<RoleDto>> GetAllRole();
    Task<RoleDto> GetRoleByRoleId(Guid id);
    Task<RoleDto> GetRoleByRoleName(string roleName);
   
}