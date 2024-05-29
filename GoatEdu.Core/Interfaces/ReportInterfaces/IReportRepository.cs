using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.ReportInterfaces;

public interface IReportRepository
{
    Task<ResponseDto> SendReport(Report report);
}