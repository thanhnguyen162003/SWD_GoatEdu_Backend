using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserDetailService _userService;
    private readonly IEnrollmentService _enrollmentService;
    private readonly IMapper _mapper;

    public UserController(IUserDetailService userService, IEnrollmentService enrollmentService, IMapper mapper)
    {
        _userService = userService;
        _enrollmentService = enrollmentService;
        _mapper = mapper;
    }
    [HttpPatch("profile")]
    [Authorize]
    public async Task<ResponseDto> UpdateProfile([FromForm] UserUpdateDto dto)
    {
        return await _userService.UpdateProfile(dto);
    }
    
    [HttpPost("subject/{id}")]
    [Authorize]
    public async Task<ResponseDto> EnrollSubject([FromRoute] Guid id)
    {
        return await _enrollmentService.EnrollUserSubject(id);
    }
    [HttpGet("enroll")]
    public async Task<IEnumerable<SubjectResponseModel>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter)
    {
        var listSubject = await _enrollmentService.GetUserEnrollments(queryFilter);
        var mapper = _mapper.Map<IEnumerable<SubjectResponseModel>>(listSubject);
        return mapper;
    }
}