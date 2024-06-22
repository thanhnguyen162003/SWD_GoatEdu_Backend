using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using GoatEdu.API.Request.TheoryViewModel;
using GoatEdu.API.Response.TheoryViewModel;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.TheoryInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/theory")]
[ApiController]
public class TheoryController : ControllerBase
{
    private readonly ITheoryService _theoryService;
    private readonly IMapper _mapper;

    public TheoryController(ITheoryService theoryService, IMapper mapper)
    {
        _theoryService = theoryService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> CreateTheory([FromForm] TheoryRequestModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<TheoryDto>(model);
            var result = await _theoryService.CreateTheory(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("{theoryId}")]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> UpdateTheory([FromRoute, Required] Guid theoryId, [FromForm] TheoryUpdateModel model)
    {
        try
        {
            var mapper = _mapper.Map<TheoryDto>(model);
            var result = await _theoryService.UpdateTheory(theoryId, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Authorize(Roles = UserEnum.MODERATOR)]
    public async Task<IActionResult> DeleteTheories([Required] IEnumerable<Guid> theoryIds)
    {
        try
        {
            var result = await _theoryService.DeleteTheory(theoryIds);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{theoryId}")]
    public async Task<IActionResult> GetTheoryById([Required] Guid theoryId)
    {
        try
        {
            var result = await _theoryService.GetTheoryById(theoryId);
            var mapper = _mapper.Map<TheoryResponseModel>(result);

            return mapper is null
                ? Ok(new ResponseDto(HttpStatusCode.NotFound, "Theory not found"))
                : Ok(new ResponseDto(HttpStatusCode.OK, "Found!", mapper));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetTheoriesByFilter(Guid? lessonId, [FromQuery, Required] TheoryQueryFilter queryFilter)
    {
        try
        {
            var result = await _theoryService.GetTheoriesByFilter(lessonId, queryFilter);
            
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

            var mapper = _mapper.Map<PagedList<TheoryResponseModel>>(result);
            
            return Ok(mapper);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}