using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
{
    private readonly GoatEduContext _context;

    public EnrollmentRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Guid> EnrollUserSubject(Enrollment enrollment)
    { 
        var result = await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();
        return result.Entity.Id;
    }
    public async Task<bool> IsUserEnrolled(Guid userId, Guid subjectId)
    {
        return await _context.Enrollments.AnyAsync(e => e.UserId == userId && e.SubjectId == subjectId);
    }
    
    public async Task<IEnumerable<Subject>> GetEnrollments(Guid userId, SubjectQueryFilter queryFilter)
    {
        var subjects = _entities
            .Where(e => e.UserId == userId)
            .Join(_context.EnrollmentProcesses,
                e => e.Id,
                ep => ep.EnrollmentId,
                (e, ep) => new { Enrollment = e, EnrollmentProcess = ep })
            .Where(joined => joined.EnrollmentProcess.Status == StatusConstraint.OPEN)
            .Join(_context.Subjects,
                joined => joined.Enrollment.SubjectId,
                s => s.Id,
                (joined, s) => s)
            .Include(x => x.Chapters.Where(chapter => chapter.IsDeleted == false))
            .AsQueryable();
        
        subjects = ApplySorting(subjects, queryFilter);
        return await subjects.ToListAsync();
    }
    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentCheck(Guid userId)
{
    return await _entities.Where(e => e.UserId == userId).ToListAsync();
}
    
    private IQueryable<Subject> ApplySorting(IQueryable<Subject> subjects, SubjectQueryFilter queryFilter)
    {
        subjects = queryFilter.sort.ToLower() switch
        {
            "name" => queryFilter.sort_direction.ToLower() == "desc"
                ? subjects.OrderByDescending(x => x.SubjectName)
                : subjects.OrderBy(x => x.SubjectName),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? subjects.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.SubjectName)
                : subjects.OrderBy(x => x.CreatedAt).ThenBy(x => x.SubjectName),
        };
        return subjects;
    }
    public async Task<Dictionary<Guid, int>> GetEnrollmentCounts()
    {
    return await _entities
    //Make a group that key is SubjectId, this group contain collection enrollment that have the key is subjectId
        .GroupBy(e => e.SubjectId)
        //the subjectId is the key, count the number of enrollment in that group
        .Select(g => new { SubjectId = g.Key, Count = g.Count() })
        //this is the return data, Guid and the number of enrollment count
        .ToDictionaryAsync(x => x.SubjectId.GetValueOrDefault(), x => x.Count);
    }
}