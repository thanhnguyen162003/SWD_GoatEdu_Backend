using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoatEdu.API.Request;
using GoatEdu.API.Request.TheoryViewModel;
using GoatEdu.API.Response;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.AnswerInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/answer")]
[ApiController]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;
    private readonly IMapper _mapper;
    private readonly IGoogleCloudService _googleCloudService;

    public AnswerController(IAnswerService answerService, IMapper mapper, IGoogleCloudService googleCloudService)
    {
        _answerService = answerService;
        _mapper = mapper;
        _googleCloudService = googleCloudService;
    }
    // public AnswerController(IAnswerService answerService, IMapper mapper)
    // {
    //     _answerService = answerService;
    //     _mapper = mapper;
    // }

    [HttpGet("discussion/{id}")]
    public async Task<IActionResult> GetAnswersByDiscussion([FromRoute, Required] Guid id,[FromQuery, Required] AnswerQueryFilter queryFilter)
    {
        try
        {
            var result = await _answerService.GetByDiscussionId(id, queryFilter);
            var mapper = _mapper.Map<PagedList<AnswerResponseModel>>(result);
            
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
            
            return Ok(mapper);
        }
        catch (Exception e)
        { 
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    
    public async Task<IActionResult> InsertAnswer([FromForm, Required] AnswerRequestModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapper = _mapper.Map<AnswerDto>(model);
            var result = await _answerService.InsertAnswer(mapper);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> DeleteNotifications(Guid id, AnswerRequestModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var mapper = _mapper.Map<AnswerDto>(model);
            var result = await _answerService.UpdateAnswer(id, mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    // [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> DeleteAnswer([Required] Guid id)
    {
        try
        {
            var result = await _answerService.DeleteAnswer(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}