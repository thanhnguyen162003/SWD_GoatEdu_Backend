using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Models;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.SubjectInterfaces;

public interface ISubjectRepository
{
    Task<ICollection<Subject>> GetAllSubjects(SubjectQueryFilter queryFilter);
    Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id);
    Task<ResponseDto> DeleteSubject(Guid id);
    Task<ResponseDto> UpdateSubject(SubjectCreateDto dto);
    Task<ResponseDto> CreateSubject(Subject dto);
    Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName);
}