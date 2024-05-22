using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;

namespace GoatEdu.Core.Interfaces.SubjectInterfaces;

public interface ISubjectService
{
    Task<ICollection<SubjectResponseDto>> GetAllSubjects();
    Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id);
    Task<ResponseDto> DeleteSubject(Guid id);
    Task<ResponseDto> UpdateSubject(SubjectCreateDto dto);
    Task<ResponseDto> CreateSubject(SubjectDto dto);
    Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName);
}