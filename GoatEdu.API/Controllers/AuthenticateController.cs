using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthenticateController(
        IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ResponseDto> Login([FromBody] LoginCredentialDto model)
    {
        return await _userService.GetLoginByCredentials(model);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<string> GetUserCurrent()
    {
        var username = User?.Identity?.Name;
        var roleId = User?.FindFirst("RoleId")?.Value;
        var userId = User?.FindFirst("UserId")?.Value;
        return Ok(new {username, userId, roleId});
    }

    [HttpPost]
    [Route("register")]
    public async Task<ResponseDto> Register([FromBody] RegisterDto model)
    {
        return await _userService.Register(model);
    }
    [HttpPost]
    [Route("login/google")]
    public async Task<ResponseDto> LoginGoogle([FromBody] LoginGoogleDto model)
    {
        return await _userService.LoginByGoogle(model);
    }
    [HttpPost]
    [Route("register/google")]
    public async Task<ResponseDto> RegisterGoogle([FromBody] GoogleRegisterDto model)
    {
        return await _userService.RegisterByGoogle(model);
    }
}