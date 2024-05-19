using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace GoatEdu.API.Controllers;

[Route("api/notification")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
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
    public async Task<IActionResult> GetPaginationNotificationByUserId([FromQuery, Required] NotificationQueryFilter queryFilter)
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
    public async Task<IActionResult> AddNotifications(List<NotificationRequestDto> requestDtos)
    {
        try
        {
            var result = await _notificationService.InsertNotification(requestDtos);
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

    [HttpDelete]
    public async Task<IActionResult> DeleteNotifications(List<Guid> ids)
    {
        try
        {
            var result = await _notificationService.DeleteNotification(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}