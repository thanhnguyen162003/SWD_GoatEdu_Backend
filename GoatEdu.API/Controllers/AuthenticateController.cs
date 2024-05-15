using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoatEdu.API.Responses;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GoatEdu.API.Controllers;

[Route("api/[controller]")]
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
    public async Task<ResponseDto> Login([FromBody] LoginDtoRequest model)
    {
        return await _userService.GetLoginByCredentials(model);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        return null;
    }
}