using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.AdminInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/admin")]
[ApiController]
[Authorize(Roles = UserEnum.ADMIN)]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpDelete("user")]
    public async Task<ResponseDto> UpdateSubject(Guid id)
    {
        return await _adminService.SuppenseUser(id);
    }
    [HttpPost("user")]
    public async Task<ResponseDto> CreateUser([FromBody] CreateUserRequestDto dto)
    {
        return await _adminService.CreateUser(dto);
    }
    [HttpGet("student")]
    public async Task<PaginatedResponse<UserMinimalDto>> GetUserUsed([FromQuery, Required] UserQueryFilter queryFilter)
    {
        return await _adminService.GetUserUsed(queryFilter);
    }
    [HttpGet("teacher")]
    public async Task<PaginatedResponse<UserMinimalDto>> GetModerator([FromQuery, Required] UserQueryFilter queryFilter)
    {
        return await _adminService.GetModerator(queryFilter);
    }
}