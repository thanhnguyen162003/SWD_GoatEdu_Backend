using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.EnrollmentInterfaces;

public interface IEnrollmentService
{
    Task<ResponseDto> EnrollUserSubject(Guid subjectId);
}