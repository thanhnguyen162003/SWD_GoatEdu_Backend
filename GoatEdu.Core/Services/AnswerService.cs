using System.Net;
using AutoMapper;
using FluentValidation;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.AnswerInterfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.SignalR;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Services.SignalR;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class AnswerService : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;
    private readonly ICurrentTime _currentTime;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IHubContext<HubService, IHubService> _hubContext;
    private readonly IValidator<AnswerDto> _validator;

    public AnswerService(IUnitOfWork unitOfWork, IClaimsService claimsService, ICurrentTime currentTime, IMapper mapper, IOptions<PaginationOptions> options, ICloudinaryService cloudinaryService, IHubContext<HubService, IHubService> hubContext, IValidator<AnswerDto> validator)
    {
        _unitOfWork = unitOfWork;
        _claimsService = claimsService;
        _currentTime = currentTime;
        _mapper = mapper;
        _paginationOptions = options.Value;
        _cloudinaryService = cloudinaryService;
        _hubContext = hubContext;
        _validator = validator;
    }

    public async Task<ResponseDto> InsertAnswer(AnswerDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }
        
        var mapper = _mapper.Map<Answer>(dto);
        
        if (dto.AnswerImageConvert != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.AnswerImageConvert);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }
        
            mapper.AnswerImage = image.Url.ToString();
        }

        mapper.AnswerVote = 0;
        mapper.AnswerName = _claimsService.GetCurrentFullname;
        mapper.CreatedAt = _currentTime.GetCurrentTime();
        mapper.CreatedBy = _claimsService.GetCurrentFullname;
        
        await _unitOfWork.AnswerRepository.AddAsync(mapper);
        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0) return new ResponseDto(HttpStatusCode.OK, "Add Failed !");
        await _hubContext.Clients.All.SendAnswer(new {dto.UserId, dto.QuestionId});
        return new ResponseDto(HttpStatusCode.OK, "Add Successfully !");

    }

    public async Task<ResponseDto> DeleteAnswer(Guid guid)
    {
        var userId = _claimsService.GetCurrentUserId;
        var answer = await _unitOfWork.AnswerRepository.GetByIdAndUserId(guid, userId);
        if (answer is null) return new ResponseDto(HttpStatusCode.BadRequest, "");
        answer.IsDeleted = false;
        _unitOfWork.AnswerRepository.Update(answer);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new ResponseDto(HttpStatusCode.OK, "Successfully!")
            : new ResponseDto(HttpStatusCode.BadRequest, "Failed!");
    }

    public async Task<PagedList<AnswerDto>> GetByDiscussionId(Guid guid, AnswerQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var lists = await _unitOfWork.AnswerRepository.GetAnswersByDiscussionIdFilters(guid, queryFilter);
        if(!lists.Any()) return new PagedList<AnswerDto>(new List<AnswerDto>(), 0, 0, 0);
        var mapperList = _mapper.Map<List<AnswerDto>>(lists);
        return PagedList<AnswerDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
    }

    public async Task<ResponseDto> UpdateAnswer(Guid answerId, AnswerDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return new ResponseDto(HttpStatusCode.BadRequest, "Validation Errors", errors);
        }
        
        var userId = _claimsService.GetCurrentUserId;
        var answer = await _unitOfWork.AnswerRepository.GetByIdAndUserId(answerId, userId);
        if (answer is null) return new ResponseDto(HttpStatusCode.NotFound, "");
        
        if (dto.AnswerImageConvert != null)
        {
            var image = await _cloudinaryService.UploadAsync(dto.AnswerImageConvert);
            if (image.Error != null)
            {
                return new ResponseDto(HttpStatusCode.BadRequest, image.Error.Message);
            }
        
            answer.AnswerImage = image.Url.ToString();
        }

        answer.AnswerBody = dto.AnswerBody ?? answer.AnswerBody;
        answer.AnswerBodyHtml = dto.AnswerBodyHtml ?? answer.AnswerBodyHtml;
        answer.UpdatedAt = _currentTime.GetCurrentTime();
        answer.UpdatedBy = _claimsService.GetCurrentFullname;
        
        _unitOfWork.AnswerRepository.Update(answer);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0 ? new ResponseDto(HttpStatusCode.OK,"Update Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Update Failed!");
    }
}