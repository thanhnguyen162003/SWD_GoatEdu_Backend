using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.EnrollmentDto;

namespace GoatEdu.Core.Interfaces.EnrollmentInterfaces;

public interface IEnrollmentProcessRepository
{
    Task<ResponseDto> CreateProcess(EnrollmentProcessDto dto);
}