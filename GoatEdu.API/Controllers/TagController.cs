using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.TagInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/tag")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public TagController(ITagService tagService, IMapper mapper)
    {
        _tagService = tagService;
        _mapper = mapper;
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetDetailsTagById([Required] Guid id)
    {
        try
        {
            var result = await _tagService.GetTagById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("name")]
    [Authorize]
    public async Task<IActionResult> GetTagByName([FromQuery, Required] string name)
    {
        try
        {
            var result = await _tagService.GetTagByName(name);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("query")]
    public async Task<IActionResult> GetTagByFilter([FromQuery, Required] TagQueryFilter queryFilter)
    {
        try
        {
            var result = await _tagService.GetTagByFilter(queryFilter);
            
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
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> AddTag(List<TagRequestModel> models)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<List<TagDto>>(models);
            var result = await _tagService.InsertTags(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> DeleteTags(List<Guid> ids)
    {
        try
        {
            var result = await _tagService.DeleteTags(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("{id}")]
    [Authorize (Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateTag(Guid id, TagUpdateModel model)
    {
        try
        {
            var mapper = _mapper.Map<TagDto>(model);
            var result = await _tagService.UpdateTag(id, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}