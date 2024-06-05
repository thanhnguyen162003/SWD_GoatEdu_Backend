using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ReportDto;

namespace GoatEdu.Core.Interfaces.ReportInterfaces;

public interface IReportService
{
    Task<ResponseDto> SendReport(ReportDto dto);
}