using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.DTOs;
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
    private readonly IValidator<SubjectCreateDto> _validator;
    private readonly IMapper _mapper;


    public SubjectController(ISubjectService subjectService, IValidator<SubjectCreateDto> validator, IMapper mapper)
    {
        _subjectService = subjectService;
        _validator = validator;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<SubjectResponseDto>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter)
    {
        var listSubject = await _subjectService.GetAllSubjects(queryFilter);
        var mapper = _mapper.Map<IEnumerable<SubjectResponseDto>>(listSubject);
        return mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<SubjectResponseDto> GetSubjectById(Guid id)
    {
        var subject = await _subjectService.GetSubjectBySubjectId(id);
        var mapper = _mapper.Map<SubjectResponseDto>(subject);
        return mapper;
    }
    [HttpGet("name")]
    public async Task<SubjectResponseDto> GetSubjectByName([FromQuery] string subjectName)
    {
        var subject = await _subjectService.GetSubjectBySubjectName(subjectName);
        var mapper = _mapper.Map<SubjectResponseDto>(subject);
        return mapper;
    }
    [HttpGet("{id}/chapters")]
    public async Task<ICollection<ChapterResponseDto>> GetChapterBySubject([FromRoute] Guid id)
    {
        var list = await _subjectService.GetChaptersBySubject(id);
        var mapper = _mapper.Map<ICollection<ChapterResponseDto>>(list);
        return mapper;
    }
    [HttpDelete("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        return await _subjectService.DeleteSubject(id);
    }
    [HttpPost]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateSubject([FromForm] SubjectCreateDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(new { Status = 400, Message = "Validation Errors", Errors = errors });
        }

        var mapper = _mapper.Map<SubjectDto>(dto);
        var result = await _subjectService.CreateSubject(mapper);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateSubject([FromForm] SubjectCreateDto dto, Guid id)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(new { Status = 400, Message = "Validation Errors", Errors = errors });
        }

        var mapper = _mapper.Map<SubjectDto>(dto);
        var result = await _subjectService.UpdateSubject(mapper, id);
        return Ok(result);
    }
}