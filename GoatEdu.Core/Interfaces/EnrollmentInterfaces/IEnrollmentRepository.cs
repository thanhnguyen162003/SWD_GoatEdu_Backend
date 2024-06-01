using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.EnrollmentInterfaces;

public interface IEnrollmentRepository
{
    Task<Enrollment> EnrollUserSubject(Enrollment enrollment);
}