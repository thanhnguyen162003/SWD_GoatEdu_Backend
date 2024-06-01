using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
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
    private readonly IEnrollmentService _enrollmentService;


    public UserController(IUserDetailService userService, IEnrollmentService enrollmentService)
    {
        _userService = userService;
        _enrollmentService = enrollmentService;
    }
    [HttpPut("profile")]
    [Authorize]
    public async Task<ResponseDto> UpdateSubject([FromForm] UserUpdateDto dto)
    {
        return await _userService.UpdateProfile(dto);
    }
    
    [HttpPost("subject/{id}")]
    [Authorize]
    public async Task<ResponseDto> UpdateSubject([FromRoute] Guid id)
    {
        return await _enrollmentService.EnrollUserSubject(id);
    }
}