using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.EnrollmentDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;

    public EnrollmentService(IUnitOfWork unitOfWork,IClaimsService claimsService,IMapper mapper, IOptions<PaginationOptions> paginationOptions)
    {
        _unitOfWork = unitOfWork;
        _claimsService = claimsService;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
    }
    public async Task<ResponseDto> EnrollUserSubject(Guid subjectId)
    {
        var userId = _claimsService.GetCurrentUserId;
        // if already enroll, dont access user to enroll
        bool isAlreadyEnrolled = await _unitOfWork.EnrollmentRepository.IsUserEnrolled(userId, subjectId);

        if (isAlreadyEnrolled)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "User is already enrolled in this subject.");
        }
        Enrollment enrollment = new Enrollment()
        {
            SubjectId = subjectId,
            UserId = userId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var result =  await _unitOfWork.EnrollmentRepository.EnrollUserSubject(enrollment);
        if (result == Guid.Empty)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Some things have error!!!");
        }

        EnrollmentProcessDto process = new EnrollmentProcessDto()
        {
            enrollmentId = result,
            process = 0
        };
        await _unitOfWork.EnrollmentProcessRepository.CreateProcess(process);
        return new ResponseDto(HttpStatusCode.OK, "Enroll Success, Happy learning");
    }

    public async Task<IEnumerable<SubjectDto>> GetUserEnrollments(SubjectQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;
        var userId = _claimsService.GetCurrentUserId;
        var listSubject = await _unitOfWork.EnrollmentRepository.GetEnrollments(userId, queryFilter);
        if (!listSubject.Any())
        {
            return new PagedList<SubjectDto>(new List<SubjectDto>(), 0, 0, 0);
        }
        var mapperList = _mapper.Map<List<SubjectDto>>(listSubject);
        return PagedList<SubjectDto>.Create(mapperList, queryFilter.page_number, queryFilter.page_size);
        
    }
}