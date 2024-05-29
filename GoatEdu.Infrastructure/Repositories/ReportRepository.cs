using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.ReportInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ReportRepository : BaseRepository<Report>,IReportRepository
{
    private readonly GoatEduContext _context;
   
    public ReportRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ResponseDto> SendReport(Report report)
    {
        _entities.Add(report);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Report successfully created.");
    }
}