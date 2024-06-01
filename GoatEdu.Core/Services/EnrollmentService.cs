using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.EnrollmentDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;

    public EnrollmentService(IUnitOfWork unitOfWork,IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _claimsService = claimsService;
    }
    public async Task<ResponseDto> EnrollUserSubject(Guid subjectId)
    {
        var userId = _claimsService.GetCurrentUserId;
        Enrollment enrollment = new Enrollment()
        {
            SubjectId = subjectId,
            UserId = userId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var result =  await _unitOfWork.EnrollmentRepository.EnrollUserSubject(enrollment);
        if (result != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Some things have error!!!");
        }

        EnrollmentProcessDto process = new EnrollmentProcessDto()
        {
            enrollmentId = result.Id,
            process = 0
        };
        await _unitOfWork.EnrollmentProcessRepository.CreateProcess(process);
        return new ResponseDto(HttpStatusCode.OK, "Enroll Success, Happy learning");
    }
}