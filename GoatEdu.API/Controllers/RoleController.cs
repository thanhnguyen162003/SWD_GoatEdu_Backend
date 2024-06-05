using AutoMapper;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ICollection<RoleResponseDto>> GetAllRoles()
    {
        var listRole =  await _roleService.GetAllRole();
        var mapper = _mapper.Map<ICollection<RoleResponseDto>>(listRole);
        return mapper;
    }
    [HttpGet("{id}")]
    public async Task<RoleResponseDto> GetRoleById([FromRoute] Guid id)
    {
        var role = await _roleService.GetRoleByRoleId(id);
        var mapper = _mapper.Map<RoleResponseDto>(role);
        return mapper;
    }
    [HttpGet("name")]
    public async Task<RoleResponseDto> GetRoleByName(string name)
    {
        var role = await _roleService.GetRoleByRoleName(name);
        var mapper = _mapper.Map<RoleResponseDto>(role);
        return mapper;
    }
    
    //paging later
    [HttpGet("user/{id}")]
    [Authorize]
    public async Task<UserRoleDto> GetUserInRole([FromRoute] Guid id)
    {
        return await _roleService.GetUsersInRole(id);
    }
}