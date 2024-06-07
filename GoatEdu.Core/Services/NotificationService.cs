using System.Net;
using AutoMapper;
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
    private readonly IHubContext<HubService, IHubService> _hubContext;

    public NotificationService(IUnitOfWork unitOfWork,IClaimsService claimsService, ICurrentTime currentTime, IMapper mapper, IOptions<PaginationOptions> options, IHubContext<HubService, IHubService> hubContext)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
        _paginationOptions = options.Value;
        _claimsService = claimsService;
        _hubContext = hubContext;
    }
    
    public async Task<ResponseDto> GetNotificationById(Guid id)
    {
        var notiFound = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
        if (notiFound != null)
        {
            notiFound.ReadAt ??= _currentTime.GetCurrentTime();
            await _unitOfWork.SaveChangesAsync();
            var notiMapper = _mapper.Map<NotificationDto>(notiFound);
            return new ResponseDto(HttpStatusCode.OK, "", notiMapper);
        }
        return new ResponseDto(HttpStatusCode.OK, "Kiếm không ra :))");
    }

    public async Task<PagedList<NotificationDto>> GetNotificationByCurrentUser(NotificationQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        var userId = _claimsService.GetCurrentUserId;
        var listNoti = await _unitOfWork.NotificationRepository.GetNotificationByUserId(userId);

        if (!listNoti.Any())
        {
            return new PagedList<NotificationDto>(new List<NotificationDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<NotificationDto>>(listNoti);
        return PagedList<NotificationDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> InsertNotification(NotificationDto notification)
    {
        var noti = _mapper.Map<Notification>(notification);
        noti.CreatedAt = _currentTime.GetCurrentTime();
        await _unitOfWork.NotificationRepository.AddAsync(noti);
        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0) return new ResponseDto(HttpStatusCode.OK, "Add Failed !");
        await _hubContext.Clients.All.SendNotification(new {notification.UserId, notification.NotificationName});
        return new ResponseDto(HttpStatusCode.OK, "Add Successfully !");
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
}