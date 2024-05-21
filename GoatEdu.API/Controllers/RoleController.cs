using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ICollection<RoleResponseDto>> GetAllRoles()
    {
        return await _roleService.GetAllRole();
    }
    [HttpGet("id/{id}")]
    public async Task<RoleResponseDto> GetRoleById([FromRoute] Guid id)
    {
        return await _roleService.GetRoleByRoleId(id);
    }
    [HttpGet("name")]
    public async Task<RoleResponseDto> GetRoleByName(string name)
    {
        return await _roleService.GetRoleByRoleName(name);
    }
}