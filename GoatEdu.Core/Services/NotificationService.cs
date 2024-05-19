using System.Net;
using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, CurrentTime currentTime, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentTime = currentTime;
        _mapper = mapper;
    }
    
    public async Task<ResponseDto> GetNotificationById(Guid id)
    {
        var notiFound = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
        if (notiFound != null)
        {
            notiFound.ReadAt = _currentTime.GetCurrentTime();
            await _unitOfWork.SaveChangesAsync();
            var notiMapper = _mapper.Map<NotificationResponseDto>(notiFound);
            return new ResponseDto(HttpStatusCode.OK, "", notiMapper);
        }
        return new ResponseDto(HttpStatusCode.OK, "Kiếm không ra :))");
    }

    public async Task<ResponseDto> GetNotificationByUserId(Guid id)
    {
        
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

    public async Task<ResponseDto> DeleteNotification(List<Guid> ids)
    {
        var notiFound = await _unitOfWork.NotificationRepository.GetNotificationByIds(ids);
        foreach (var data in notiFound)
        {
            data.IsDeleted = false;
        }
        _unitOfWork.NotificationRepository.UpdateRange(notiFound);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
        {
            return new ResponseDto(HttpStatusCode.OK, "Delete Successfully !");
        }
        return new ResponseDto(HttpStatusCode.OK, "Delete Failed !");
    }
}