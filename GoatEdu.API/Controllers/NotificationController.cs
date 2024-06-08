using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.API.Response;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace GoatEdu.API.Controllers;

[Route("api/notification")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public NotificationController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> GetDetailedNotificationById([Required] Guid id)
    {
        try
        {
            var result = await _notificationService.GetNotificationById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("user")]
    [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> GetNotificationByCurrentUser([FromQuery, Required] NotificationQueryFilter queryFilter)
    {
        try
        {
            var result = await _notificationService.GetNotificationByCurrentUser(queryFilter);
            var mapper = _mapper.Map<PagedList<NotiDetailResponseModel>>(result);
            
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
    public async Task<IActionResult> AddNotifications([Required] NotificationRequestModel requestModel)
    {
        try
        {
            // var validationResult = await _validator.ValidateAsync(requestDto);
            // if (!validationResult.IsValid)
            // {
            //     return BadRequest(new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}"));
            // }

            var mapper = _mapper.Map<NotificationDto>(requestModel);
            var result = await _notificationService.InsertNotification(mapper);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Authorize (Roles = "Student, Teacher")]
    public async Task<IActionResult> DeleteNotifications([FromQuery, Required] List<Guid> ids)
    {
        try
        {
            var result = await _notificationService.DeleteNotifications(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}