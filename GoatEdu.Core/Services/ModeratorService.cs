using System.Net;
using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.Services.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace GoatEdu.Core.Services;

public class ModeratorService : IModeratorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IHubContext<MyHub> _hubContext;

    public ModeratorService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService, IHubContext<MyHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
        _hubContext = hubContext;
    }

    public async Task<ResponseDto> ApprovedDiscussion(Guid discussionId)
    {
        var userId = await _unitOfWork.ModeratorRepository.ApprovedDiscussions(discussionId);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Approve Failed!");
        }
        
        var notification = new NotificationDto
        {
            NotificationName = "Discussion Approved!",
            NotificationMessage = "Your discussion has been reviewed and approved by the moderators.",
            UserId = userId
                
        };
        
        var save = await _notificationService.InsertNotification(notification);

        if (save.Message.Contains("Failed"))
        {
            return new ResponseDto(HttpStatusCode.OK, "Approved Successfully but Cannot Send Notification!");
        }

        var notiCount = await _unitOfWork.NotificationRepository.CountUnreadNotification((Guid)userId);
        await _hubContext.Clients.User(userId.ToString()).SendAsync("Notification" ,"You have new notification!", notiCount);
        return new ResponseDto(HttpStatusCode.OK, "Approved And Send Notification Successfully!");
    }
}