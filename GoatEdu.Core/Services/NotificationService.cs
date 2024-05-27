using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly IClaimsService _claimsService;

    public NotificationService(IUnitOfWork unitOfWork,IClaimsService claimsService, ICurrentTime currentTime, IMapper mapper, IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
        _paginationOptions = options.Value;
        _claimsService = claimsService;
    }
    
    public async Task<ResponseDto> GetNotificationById(Guid id)
    {
        var notiFound = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
        if (notiFound != null)
        {
            notiFound.ReadAt ??= _currentTime.GetCurrentTime();
            await _unitOfWork.SaveChangesAsync();
            var notiMapper = _mapper.Map<NotificationResponseDto>(notiFound);
            return new ResponseDto(HttpStatusCode.OK, "", notiMapper);
        }
        return new ResponseDto(HttpStatusCode.OK, "Kiếm không ra :))");
    }

    public async Task<PagedList<NotificationResponseDto>> GetNotificationByCurrentUser(NotificationQueryFilter queryFilter)
    {
        queryFilter.PageNumber = queryFilter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.PageNumber;
        queryFilter.PageSize = queryFilter.PageSize == 0 ? _paginationOptions.DefaultPageSize : queryFilter.PageSize;
        var userId = _claimsService.GetCurrentUserId;
        var listNoti = await _unitOfWork.NotificationRepository.GetNotificationByUserId(userId);

        if (!listNoti.Any())
        {
            return new PagedList<NotificationResponseDto>(new List<NotificationResponseDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<NotificationResponseDto>>(listNoti);
        return PagedList<NotificationResponseDto>.Create(mapperList, queryFilter.PageNumber, queryFilter.PageSize);
    }

    public async Task<ResponseDto> InsertNotification(NotificationRequestDto notification)
    {
        var noti = _mapper.Map<Notification>(notification);
        await _unitOfWork.NotificationRepository.AddAsync(noti);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Add Successfully !");
        }
        return new ResponseDto(HttpStatusCode.OK, "Add Failed !");
    }

    public async Task<ResponseDto> DeleteNotifications(List<Guid> ids)
    {
        var notiFound = await _unitOfWork.NotificationRepository.GetNotificationByIds(ids);
        _unitOfWork.NotificationRepository.DeleteAsync(notiFound);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Delete Successfully !");
        }
        return new ResponseDto(HttpStatusCode.OK, "Delete Failed !");
    }
}