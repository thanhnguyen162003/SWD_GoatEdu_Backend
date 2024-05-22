using System.Net;
using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;


[Route("api/subject")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    private readonly IValidator<SubjectDto> _validator;


    public SubjectController(ISubjectService subjectService, IValidator<SubjectDto> validator)
    {
        _subjectService = subjectService;
        _validator = validator;
    }
    [HttpGet]
    public async Task<ICollection<SubjectResponseDto>> GetAllSubject()
    {
        return await _subjectService.GetAllSubjects();
    }
    
    [HttpGet("id/{id}")]
    public async Task<SubjectResponseDto> GetSubjectById(Guid id)
    {
        return await _subjectService.GetSubjectBySubjectId(id);
    }
    [HttpDelete("id/{id}")]
    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        return await _subjectService.DeleteSubject(id);
    }
    [HttpPost]
    public async Task<ResponseDto> CreateSubject([FromBody] SubjectDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}");
        }
        return await _subjectService.CreateSubject(dto);
    }
    [HttpPut("id/{id}")]
    public async Task<ResponseDto> UpdateSubject([FromBody] SubjectCreateDto dto)
    {
        return await _subjectService.UpdateSubject(dto);
    }
}