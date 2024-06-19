using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.ChapterInterfaces;

public interface IChapterRepository : IRepository<Chapter>
{
    Task<ICollection<Chapter>> GetChapters(ChapterQueryFilter queryFilter);
    Task<ICollection<Chapter>> GetChaptersBySubject(Guid subjectId);
    Task<ChapterDto> GetChapterByChapterId(Guid id);
    Task<ResponseDto> DeleteChapter(Guid id);
    Task<ResponseDto> UpdateChapter(ChapterDto dto, Guid chapterId);
    Task<ResponseDto> CreateChapter(Chapter dto);
    Task<ChapterDto> GetChapterByChapterName(string chapterName);
    // Validation
    Task<bool> ChapterIdExistsAsync(Guid? guid);
    Task<bool> ChapterNameExistsAsync(string name);
    Task<bool> ChapterLevelExistsAsync(int? code);
    
}