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

    Task<ChapterResponseDto> GetChapterByChapterId(Guid id);
    Task<ResponseDto> DeleteChapter(Guid id);
    Task<ResponseDto> UpdateChapter(ChapterCreateDto dto);
    Task<ResponseDto> CreateChapter(Chapter dto);
    Task<ChapterResponseDto> GetChapterByChapterName(string chapterName);
    Task<bool> GetAllChapterCheck(string name);
}