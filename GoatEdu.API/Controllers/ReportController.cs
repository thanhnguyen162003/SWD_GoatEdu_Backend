using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ReportDto;
using GoatEdu.Core.Interfaces.ReportInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/report")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    
    [HttpPost]
    public async Task<ResponseDto> CreateUser([FromBody] ReportRequestDto dto)
    {
        return await _reportService.SendReport(dto);
    }
}