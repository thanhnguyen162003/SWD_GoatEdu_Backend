using System.Collections;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.QueriesFilter;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.Interfaces.SubjectInterfaces;

public interface ISubjectService
{
    Task<IEnumerable<SubjectResponseDto>> GetAllSubjects(SubjectQueryFilter queryFilter);
    Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id);
    Task<ICollection<ChapterResponseDto>> GetChaptersBySubject(Guid subjectId);
    Task<ResponseDto> DeleteSubject(Guid id);
    Task<ResponseDto> UpdateSubject(SubjectCreateDto dto);
    Task<ResponseDto> CreateSubject(SubjectDto dto);
    Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName);
}