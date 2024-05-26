using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
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
    private readonly IValidator<TagRequestDto> _validator;

    public TagController(ITagService tagService, IValidator<TagRequestDto> validator)
    {
        _tagService = tagService;
        _validator = validator;
    }
    
    [HttpGet("{id}")]
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
    
    [HttpGet("search")]
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
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> AddTag(List<TagRequestDto> tagRequestDto)
    {
        try
        {
            foreach (var data in tagRequestDto)
            {
                var validationResult = await _validator.ValidateAsync(data);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}")) ;
                }
            }
            
            var result = await _tagService.InsertTags(tagRequestDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(Guid id, TagRequestDto tagRequestDto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(tagRequestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}")) ;
            }
            var result = await _tagService.UpdateTag(id, tagRequestDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}