using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IEnumerable<SubjectResponseDto>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter)
    {
        return await _subjectService.GetAllSubjects(queryFilter);
    }
    
    [HttpGet("{id}")]
    public async Task<SubjectResponseDto> GetSubjectById(Guid id)
    {
        return await _subjectService.GetSubjectBySubjectId(id);
    }
    [HttpGet("name")]
    public async Task<SubjectResponseDto> GetSubjectByName([FromQuery] string subjectName)
    {
        return await _subjectService.GetSubjectBySubjectName(subjectName);
    }
    [HttpGet("{id}/chapters")]
    public async Task<ICollection<ChapterResponseDto>> GetChapterBySubject([FromRoute] Guid id)
    {
        return await _subjectService.GetChaptersBySubject(id);
    }
    [HttpDelete("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        return await _subjectService.DeleteSubject(id);
    }
    [HttpPost]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> CreateSubject([FromForm]SubjectDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}");
        }
        return await _subjectService.CreateSubject(dto);
    }
    [HttpPut("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> UpdateSubject([FromForm] SubjectCreateDto dto)
    {
        return await _subjectService.UpdateSubject(dto);
    }
}