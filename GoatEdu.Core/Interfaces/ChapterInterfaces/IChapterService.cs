using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.ChapterInterfaces;

public interface IChapterService
{
        Task<ICollection<ChapterResponseDto>> GetChapters(ChapterQueryFilter queryFilter);
        // Task<ChapterResponseDto> GetChaptersBySubject(ChapterQueryFilter queryFilter);
        Task<ResponseDto> DeleteChapter(Guid id);
        Task<ResponseDto> UpdateChapter(ChapterCreateDto dto);
        Task<ResponseDto> CreateChapter(ChapterDto dto);
        Task<ChapterResponseDto> GetChapterByChapterName(string chapterName);
        Task<ChapterResponseDto> GetChapterByChapterId(Guid id);
}