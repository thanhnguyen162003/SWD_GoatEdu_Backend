using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Services.SignalR;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly IClaimsService _claimsService;
    private readonly IHubContext<MyHub> _hubContext;
    private readonly IValidator<NotificationDto> _validator;

    public NotificationService(IUnitOfWork unitOfWork,IClaimsService claimsService, ICurrentTime currentTime, IMapper mapper, IOptions<PaginationOptions> options, IValidator<NotificationDto> validator, IHubContext<MyHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
        _paginationOptions = options.Value;
        _claimsService = claimsService;
        _hubContext = hubContext;
        _validator = validator;
    }
    
    public async Task<ResponseDto> MarkReadAllNotifications()
    {
        var userId = _claimsService.GetCurrentUserId;
        var notifications = await _unitOfWork.NotificationRepository.GetNotificationsByUserId(userId);
        if (!notifications.Any())
        {
            return new ResponseDto(HttpStatusCode.OK, "You dont have permission!");
        }
        foreach (var noti in notifications)
        {
            noti.ReadAt ??= _currentTime.GetCurrentTime();
        }
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Mark Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Mark Failed!");
    }

    public async Task<ResponseDto> MarkReadNotification(Guid notificationId)
    {
        var userId = _claimsService.GetCurrentUserId;
        var notification = await _unitOfWork.NotificationRepository.GetNotificationByUserId(userId, notificationId);
        if (notification is null)
        {
            return new ResponseDto(HttpStatusCode.OK, "You dont have permission!");
        }

        notification.ReadAt ??= _currentTime.GetCurrentTime();
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Mark Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Mark Failed!");
    }

    public async Task<PagedList<NotificationDto>> GetNotificationByCurrentUser(NotificationQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        var userId = _claimsService.GetCurrentUserId;
        var listNoti = await _unitOfWork.NotificationRepository.GetNotificationsByUserId(userId);

        if (!listNoti.Any())
        {
            return new PagedList<NotificationDto>(new List<NotificationDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<NotificationDto>>(listNoti);
        return PagedList<NotificationDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> InsertNotification(NotificationDto notification)
    {
        var validationResult = await _validator.ValidateAsync(notification);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }

        var userId = notification.UserId.ToString();
        var noti = _mapper.Map<Notification>(notification);
        noti.CreatedAt = _currentTime.GetCurrentTime();
        await _unitOfWork.NotificationRepository.AddAsync(noti);
        var result = await _unitOfWork.SaveChangesAsync();

        return result <= 0 ? new ResponseDto(HttpStatusCode.OK, "Add Failed !") :
            // await _hubContext.Clients.User(userId).SendNotification("You have new notification!");
            new ResponseDto(HttpStatusCode.OK, "Add Successfully !");
    }

    public async Task<ResponseDto> DeleteNotifications(List<Guid> ids)
    {
        var userId = _claimsService.GetCurrentUserId;
        var notiFound = await _unitOfWork.NotificationRepository.GetNotificationByIds(ids, userId);
        _unitOfWork.NotificationRepository.DeleteAsync(notiFound);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Delete Successfully !");
        }
        return new ResponseDto(HttpStatusCode.OK, "Delete Failed !");
    }

    public async Task SendNotification(Guid userId)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync("Notification", "You have new notification!");
    }
}