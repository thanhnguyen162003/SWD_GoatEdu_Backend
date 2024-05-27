using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
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
    private readonly IValidator<DiscussionRequestDto> _validator;

    public DiscussionController(IDiscussionService discussionService, IValidator<DiscussionRequestDto> validator)
    {
        _discussionService = discussionService;
        _validator = validator;
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
            
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> AddDiscussion(DiscussionRequestDto discussionRequestDto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(discussionRequestDto);
            if (!validationResult.IsValid)
            { 
                return BadRequest(new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}")) ;
            }
            
            var result = await _discussionService.InsertDiscussion(discussionRequestDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> DeleteDiscussions(List<Guid> ids)
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
    [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> UpdateDiscussion(Guid id, DiscussionRequestDto discussionRequestDto)
    {
        try
        {
            var result = await _discussionService.UpdateDiscussion(id, discussionRequestDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}