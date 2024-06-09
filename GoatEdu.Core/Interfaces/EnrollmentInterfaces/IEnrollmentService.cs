using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.EnrollmentInterfaces;

public interface IEnrollmentService
{
    Task<ResponseDto> EnrollUserSubject(Guid subjectId);
    Task<IEnumerable<SubjectDto>> GetUserEnrollments(SubjectQueryFilter queryFilter);
}