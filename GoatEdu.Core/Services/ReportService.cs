using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ReportDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.ReportInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;

    public ReportService(IUnitOfWork unitOfWork, IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _claimsService = claimsService;
    }
    
    
    public async Task<ResponseDto> SendReport(ReportDto dto)
    {
        var userId = _claimsService.GetCurrentUserId;
        var fullname = _claimsService.GetCurrentFullname;
        Report report = new Report()
        {
            ReportTitle = dto.ReportTitle,
            ReportContent = dto.ReportContent,
            CreatedAt = DateTime.Now,
            CreatedBy = fullname,
            UserId = userId,
            Status = StatusConstraint.OPEN,
            IsDeleted = false
        };
         await _unitOfWork.ReportRepository.SendReport(report);
         return new ResponseDto(HttpStatusCode.OK, "Success Send Report", dto);

    }
}