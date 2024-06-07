using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.SubjectInterfaces;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetAllSubjects(SubjectQueryFilter queryFilter);
    Task<SubjectDto> GetSubjectBySubjectId(Guid id);
    Task<ResponseDto> DeleteSubject(Guid id);
    Task<ResponseDto> UpdateSubject(Subject dto);
    Task<ResponseDto> CreateSubject(Subject dto);
    Task<SubjectDto> GetSubjectBySubjectName(string subjectName);
    Task<IEnumerable<Subject>> GetSubjectByClass(string classes, SubjectQueryFilter queryFilter);
}