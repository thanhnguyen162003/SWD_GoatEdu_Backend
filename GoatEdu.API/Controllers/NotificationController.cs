using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using FluentValidation;
using GoatEdu.API.Responses;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Validator;
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
    private readonly IValidator<NotificationRequestDto> _validator;

    public NotificationController(INotificationService notificationService,
        IValidator<NotificationRequestDto> validator)
    {
        _notificationService = notificationService;
        _validator = validator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailedNotificationById([Required] Guid id)
    {
        try
        {
            var result = await _notificationService.GetNotificationById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
            {
                return BadRequest(e.InnerException.Message);
            }

            return BadRequest(e.Message);
        }
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetNotificationByUserId([FromQuery, Required] NotificationQueryFilter queryFilter)
    {
        try
        {
            var result = await _notificationService.GetNotificationByUserId(queryFilter);

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
    public async Task<IActionResult> AddNotifications(NotificationRequestDto requestDto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDto(HttpStatusCode.BadRequest, $"{validationResult.Errors}"));
            }

            var result = await _notificationService.InsertNotification(requestDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteNotifications(List<Guid> ids)
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