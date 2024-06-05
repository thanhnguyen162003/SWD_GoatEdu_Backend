using System.Collections;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.Interfaces.SubjectInterfaces;

public interface ISubjectService
{
    Task<IEnumerable<SubjectDto>> GetAllSubjects(SubjectQueryFilter queryFilter);
    Task<SubjectDto> GetSubjectBySubjectId(Guid id);
    Task<ICollection<SubjectDto>> GetChaptersBySubject(Guid subjectId);
    Task<ResponseDto> DeleteSubject(Guid id);
    Task<ResponseDto> UpdateSubject(SubjectDto dto, Guid id);
    Task<ResponseDto> CreateSubject(SubjectDto dto);
    Task<SubjectDto> GetSubjectBySubjectName(string subjectName);
}