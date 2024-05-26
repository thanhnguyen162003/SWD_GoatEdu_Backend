using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ICollection<RoleResponseDto>> GetAllRole()
    {
        return await _unitOfWork.RoleRepository.GetAllRole();
    }

    public async Task<RoleResponseDto> GetRoleByRoleId(Guid id)
    {
        return await _unitOfWork.RoleRepository.GetRoleByRoleId(id);
    }

    public async Task<RoleResponseDto> GetRoleByRoleName(string roleName)
    {
        return await _unitOfWork.RoleRepository.GetRoleByRoleName(roleName);
    }

    public async Task<UserRoleDto> GetUsersInRole(Guid roleId)
    {
        // Fetch the role to get the role name
        var role = await _unitOfWork.RoleRepository.GetRoleByRoleId(roleId);

        if (role == null)
        {
            throw new KeyNotFoundException("Role not found.");
        }

        // Fetch users in the specific role
        var users = await _unitOfWork.UserRepository.GetUsersInRole(roleId);

        // Map users to UserMinimalDto and create UserRoleDto
        var userRoleDto = new UserRoleDto
        {
            RoleName = role.RoleName,
            UserMinimalDtos = users
        };

        return userRoleDto;
    }
}