using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.DiscussionInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/discussion")]
[ApiController]
public class DiscussionController : ControllerBase
{
    private readonly IDiscussionService _discussionService;
    private readonly IMapper _mapper;

    public DiscussionController(IDiscussionService discussionService, IMapper mapper)
    {
        _discussionService = discussionService;
        _mapper = mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailsDiscussionById([Required] Guid id)
    {
        try
        {
            var result = await _discussionService.GetDiscussionById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDetailsDiscussionByFilter([FromQuery, Required] DiscussionQueryFilter queryFilter)
    {
        try
        {
            
            var result = await _discussionService.GetDiscussionByFilter(queryFilter);
            
            var metadata = new Metadata
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetDetailsDiscussionByCurrentUser([FromQuery, Required] DiscussionQueryFilter queryFilter)
    {
        try
        {
            var result = await _discussionService.GetDiscussionByUserId(queryFilter);
            
            var metadata = new Metadata
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            var mapper = _mapper.Map<DiscussionResponseModel>(result);
            
            return Ok(mapper);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize (Roles = $"{UserEnum.STUDENT}, {UserEnum.TEACHER}")]
    public async Task<IActionResult> AddDiscussion([FromForm]DiscussionRequestModel discussionRequestModel)
    {
        try
        {
            var tagsJson = Request.Form["Tags"];
            discussionRequestModel.Tags = JsonConvert.DeserializeObject<List<TagRequestModel>>(tagsJson);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapper = _mapper.Map<DiscussionDto>(discussionRequestModel);
            var result = await _discussionService.InsertDiscussion(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize (Roles = $"{UserEnum.MODERATOR},{UserEnum.STUDENT},{UserEnum.TEACHER}")]
    public async Task<IActionResult> DeleteDiscussions([FromQuery, Required] List<Guid> ids)
    {
        try
        {
            var result = await _discussionService.DeleteDiscussions(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut ("{id}")]
    [Authorize (Roles = $"{UserEnum.STUDENT}, {UserEnum.TEACHER}")]
    public async Task<IActionResult> UpdateDiscussion(Guid id, DiscussionRequestModel discussionRequestModel)
    {
        try
        {
            var tagsJson = Request.Form["Tags"];
            discussionRequestModel.Tags = JsonConvert.DeserializeObject<List<TagRequestModel>>(tagsJson);
            
            var mapper = _mapper.Map<DiscussionUpdateDto>(discussionRequestModel);
            var result = await _discussionService.UpdateDiscussion(id, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}