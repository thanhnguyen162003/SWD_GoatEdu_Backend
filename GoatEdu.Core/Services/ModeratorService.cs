using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Services.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class ModeratorService : IModeratorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IHubContext<MyHub> _hubContext;
    private readonly PaginationOptions _paginationOptions;

    public ModeratorService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService, IHubContext<MyHub> hubContext, IOptions<PaginationOptions> options)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
        _hubContext = hubContext;
        _paginationOptions = options.Value;
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

    public async Task<PagedList<SubjectDto>> GetSubjectByClass(SubjectQueryFilter queryFilter, string classes)
    {
        queryFilter.page_number =
            queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var listSubject = await _unitOfWork.SubjectRepository.GetSubjectByClass(classes, queryFilter);

        if (!listSubject.Any())
        {
            return new PagedList<SubjectDto>(new List<SubjectDto>(), 0, 0, 0);
        }
        
        var mapperList = _mapper.Map<List<SubjectDto>>(listSubject);
        return PagedList<SubjectDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);    
    }

    public async Task<SubjectDto> GetSubjectBySubjectId(Guid id)
    {
        var subject = await _unitOfWork.SubjectRepository.GetSubjectBySubjectId(id);
        
        var subjectDto = _mapper.Map<SubjectDto>(subject);
        
        return subjectDto;
    }
}