using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
{
    private readonly GoatEduContext _context;

    public EnrollmentRepository(GoatEduContext context) : base(context)
    {
        _context = _context;
    }

    public async Task<Enrollment> EnrollUserSubject(Enrollment enrollment)
    {
        var result = await _entities.AddAsync(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }
}