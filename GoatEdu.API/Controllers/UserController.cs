using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentEmail.Core;
using GoatEdu.API.Request;
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
    
    [HttpPatch("new_user")]
    [Authorize]
    public async Task<ResponseDto> UpdateNewUser()
    {
        return await _userService.UpdateNewUser();
    }
    [HttpPatch("password")]
    [Authorize]
    public async Task<ResponseDto> UpdatePassword([FromBody] ChangePasswordModel model)
    {
        var oldpassword = model.old_password;
        var newpassword = model.new_password;
        return await _userService.UpdatePassword(oldpassword, newpassword);
    }
    
    [HttpPost("subject/{id}")]
    [Authorize]
    public async Task<ResponseDto> EnrollSubject([FromRoute] Guid id)
    {
        return await _enrollmentService.EnrollUserSubject(id);
    }
    [HttpGet("enroll")]
    public async Task<SubjectEnrollResponseModel> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter)
    {
        var listSubject = await _enrollmentService.GetUserEnrollments(queryFilter);
        // var mapperSubjectDto = _mapper.Map<IEnumerable<SubjectResponseModel>>(listSubject);
        var mapperSubjectDto = listSubject.Select(x =>
        {
            var subjectDto = _mapper.Map<SubjectResponseModel>(x);
            subjectDto.IsEnroll = true;
            return subjectDto;
        });
        var mapper = _mapper.Map<SubjectEnrollResponseModel>(mapperSubjectDto);
        return mapper;
    }
}