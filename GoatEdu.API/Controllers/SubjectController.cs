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

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
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
}