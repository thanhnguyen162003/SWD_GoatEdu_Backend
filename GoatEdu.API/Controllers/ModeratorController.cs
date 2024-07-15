using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Response;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/moder")]
[ApiController]
[Authorize(Roles = UserEnum.MODERATOR)]
public class ModeratorController : ControllerBase
{
    private readonly IModeratorService _moderatorService;
    private readonly IMapper _mapper;

    public ModeratorController(IModeratorService moderatorService, IMapper mapper)
    {
        _moderatorService = moderatorService;
        _mapper = mapper;
    }

    [HttpPost("discussion/{discussionId}")]
    public async Task<IActionResult> ApproveDiscussion([FromRoute, Required] Guid discussionId)
    {
        try
        {
            var result = await _moderatorService.ApprovedDiscussion(discussionId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("subject/class")]
    public async Task<IEnumerable<SubjectResponseModel>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter, [FromQuery,Required] string classes)
    {
        var listSubject = await _moderatorService.GetSubjectByClass(queryFilter, classes);
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
    
    [HttpGet("subject/{id}")]
    public async Task<SubjectDetailResponseModel> GetSubjectById(Guid id)
    {
        var subject = await _moderatorService.GetSubjectBySubjectId(id);
        var mapper = _mapper.Map<SubjectDetailResponseModel>(subject);
        return mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<SubjectResponseModel>> GetAllSubject([FromQuery, Required] SubjectQueryFilter queryFilter)
    {
        var listSubject = await _moderatorService.GetAllSubjects(queryFilter);
        
        
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
}