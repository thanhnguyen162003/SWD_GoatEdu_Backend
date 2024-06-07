using AutoMapper;
using GoatEdu.API.Request;
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
    private readonly IMapper _mapper;

    public ReportController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }
    
    
    [HttpPost]
    public async Task<ResponseDto> CreateUser([FromBody] ReportRequestModel model)
    {
        var mapper = _mapper.Map<ReportDto>(model);
        return await _reportService.SendReport(mapper);
    }
}