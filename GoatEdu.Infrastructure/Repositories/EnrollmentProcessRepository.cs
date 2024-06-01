using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.EnrollmentDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EnrollmentProcessRepository : BaseRepository<EnrollmentProcess>,IEnrollmentProcessRepository
{
    private readonly GoatEduContext _context;

    public EnrollmentProcessRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<ResponseDto> CreateProcess(EnrollmentProcessDto dto)
    {
        EnrollmentProcess process = new EnrollmentProcess()
        {
            EnrollmentId = dto.enrollmentId,
            Process = dto.process,
            Status = StatusConstraint.OPEN,
            ChapterId = null
        };
        var result = await _entities.AddAsync(process);
        await _context.SaveChangesAsync(); // Await SaveChangesAsync
        return new ResponseDto(HttpStatusCode.OK,"Generate Process Ok");
    }
}