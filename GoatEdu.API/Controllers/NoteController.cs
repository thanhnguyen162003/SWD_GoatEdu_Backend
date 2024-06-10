using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Validator;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/note")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IMapper _mapper;

    public NoteController(INoteService noteService, IMapper mapper)
    {
        _noteService = noteService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetDetailsNoteById([Required] Guid id)
    {
        try
        {
            var result = await _noteService.GetNoteById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetNoteByFilter([FromQuery, Required] NoteQueryFilter queryFilter)
    {
        try
        {
            var result = await _noteService.GetNoteByFilter(queryFilter);
            
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
    [Authorize]
    public async Task<IActionResult> CreateNote([Required] NoteRequestModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<NoteDto>(model);
            var result = await _noteService.CreateNote(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteNotes([FromQuery, Required] List<Guid> ids)
    {
        try
        {
            var result = await _noteService.DeleteNotes(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateNote([FromRoute, Required] Guid id, [Required] NoteUpdateModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<NoteDto>(model);
            var result = await _noteService.UpdateNote(id, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    
}