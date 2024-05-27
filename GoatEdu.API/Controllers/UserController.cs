using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserDetailService _userService;

    public UserController(IUserDetailService userService)
    {
        _userService = userService;
    }
    [HttpPut("profile")]
    [Authorize]
    public async Task<ResponseDto> UpdateSubject([FromForm] UserUpdateDto dto)
    {
        return await _userService.UpdateProfile(dto);
    }
}