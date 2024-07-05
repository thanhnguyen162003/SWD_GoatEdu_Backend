using GoatEdu.Core.DTOs;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.EnrollmentInterfaces;

public interface IEnrollmentRepository
{
    Task<Guid> EnrollUserSubject(Enrollment enrollment);
    Task<bool> IsUserEnrolled(Guid userId, Guid subjectId);
    Task<IEnumerable<Subject>> GetEnrollments(Guid userId, SubjectQueryFilter queryFilter);
    Task<IEnumerable<Enrollment>> GetAllEnrollmentCheck(Guid userId);
    Task<Dictionary<Guid, int>> GetEnrollmentCounts();
}