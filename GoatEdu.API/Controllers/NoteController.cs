using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Validator;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/note")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IValidator<NoteRequestDto> _validator;
    private readonly IMapper _mapper;

    public NoteController(INoteService noteService, IValidator<NoteRequestDto> validator, IMapper mapper)
    {
        _noteService = noteService;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
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
    public async Task<IActionResult> AddNote([Required] NoteRequestDto noteRequestDto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(noteRequestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}")) ;
            }

            var mapper = _mapper.Map<NoteDto>(noteRequestDto);
            var result = await _noteService.InsertNote(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
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
}