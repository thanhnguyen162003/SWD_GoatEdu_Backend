using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.ChapterInterfaces;

public interface IChapterService
{
        Task<ICollection<ChapterDto>> GetChapters(ChapterQueryFilter queryFilter);
        Task<ResponseDto> DeleteChapter(Guid id);
        Task<ResponseDto> UpdateChapter(ChapterDto dto, Guid chapterId);
        Task<ResponseDto> CreateChapter(ChapterDto dto);
        Task<ChapterDto> GetChapterByChapterName(string chapterName);
        Task<ChapterDto> GetChapterByChapterId(Guid id);
}