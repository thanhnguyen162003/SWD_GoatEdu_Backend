using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Models;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.ChapterInterfaces;

public interface IChapterRepository
{
    Task<ICollection<Chapter>> GetChapters(ChapterQueryFilter queryFilter);
    Task<ICollection<Chapter>> GetChaptersBySubject(ChapterQueryFilter queryFilter);

    Task<ChapterResponseDto> GetChapterByChapterId(Guid id);
    Task<ResponseDto> DeleteChapter(Guid id);
    Task<ResponseDto> UpdateChapter(ChapterCreateDto dto);
    Task<ResponseDto> CreateChapter(Chapter dto);
    Task<ChapterResponseDto> GetChapterByChapterName(string chapterName);
    Task<bool> GetAllChapterCheck(string name);
}