using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;


[Route("api/subject")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    private readonly IMapper _mapper;


    public SubjectController(ISubjectService subjectService, IMapper mapper)
    {
        _subjectService = subjectService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<SubjectResponseModel>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter)
    {
        var listSubject = await _subjectService.GetAllSubjects(queryFilter);
        
        
        var metadata = new Metadata
        {
            TotalCount = listSubject.TotalCount,
            PageSize = listSubject.PageSize,
            CurrentPage = listSubject.CurrentPage,
            TotalPages = listSubject.TotalPages,
            HasNextPage = listSubject.HasNextPage,
            HasPreviousPage = listSubject.HasPreviousPage
        };
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        
        var mapper = _mapper.Map<IEnumerable<SubjectResponseModel>>(listSubject);
        
        return mapper;
    }
    [HttpGet("class")]
    public async Task<IEnumerable<SubjectResponseModel>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter, [FromQuery,Required] string classes)
    {
        var listSubject = await _subjectService.GetSubjectByClass(queryFilter, classes);
        var mapper = _mapper.Map<IEnumerable<SubjectResponseModel>>(listSubject);
        return mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<SubjectDetailResponseModel> GetSubjectById(Guid id)
    {
        var subject = await _subjectService.GetSubjectBySubjectId(id);
        var mapper = _mapper.Map<SubjectDetailResponseModel>(subject);
        return mapper;
    }
    [HttpGet("name")]
    public async Task<SubjectResponseModel> GetSubjectByName([FromQuery] string subjectName)
    {
        var subject = await _subjectService.GetSubjectBySubjectName(subjectName);
        var mapper = _mapper.Map<SubjectResponseModel>(subject);
        return mapper;
    }
    [HttpGet("{id}/chapters")]
    public async Task<ICollection<ChapterResponseModel>> GetChapterBySubject([FromRoute] Guid id)
    {
        var list = await _subjectService.GetChaptersBySubject(id);
        var mapper = _mapper.Map<ICollection<ChapterResponseModel>>(list);
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
    public async Task<IActionResult> CreateSubject([FromForm] SubjectCreateModel model)
    {
        var mapper = _mapper.Map<SubjectDto>(model);
        var result = await _subjectService.CreateSubject(mapper);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateSubject([FromForm] SubjectCreateModel model, Guid id)
    {
        var mapper = _mapper.Map<SubjectDto>(model);
        var result = await _subjectService.UpdateSubject(mapper, id);
        return Ok(result);
    }
}
