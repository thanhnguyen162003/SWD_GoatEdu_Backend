using System.Net;
using EntityFramework.Extensions;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.Models;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChapterRepository : BaseRepository<Chapter>, IChapterRepository
{
    private readonly GoatEduContext _context;

    public ChapterRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<Chapter>> GetChapters(ChapterQueryFilter queryFilter)
    {
        var chapters = _entities.AsQueryable();
        chapters = ApplyFilterSortAndSearch(chapters, queryFilter);
        chapters = ApplySorting(chapters, queryFilter);
        return await chapters.ToListAsync();
    }

    public async Task<ICollection<Chapter>> GetChaptersBySubject(ChapterQueryFilter queryFilter)
    {
        return await _entities
            .AsNoTracking()
            .Where(c => c.SubjectId == queryFilter.SubjectId && c.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<ChapterResponseDto> GetChapterByChapterId(Guid id)
    {
        return await _entities.AsNoTracking()
            .Where(c => c.Id == id && c.IsDeleted == false)
            .Select(c => new ChapterResponseDto()
            {
                Id = c.Id,
                ChapterName = c.ChapterName,
                ChapterLevel = c.ChapterLevel,
                SubjectId = c.SubjectId,
                CreatedAt = c.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<ResponseDto> DeleteChapter(Guid id)
    {
        var chapter = await _entities.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (chapter == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Chapter does not exist anymore!!!");
        }

        // Set IsDeleted to true
        chapter.IsDeleted = true;

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return a successful response
        return new ResponseDto(HttpStatusCode.OK, "Chapter successfully deleted.");
    }

    public async Task<bool> GetAllChapterCheck(string name)
    {
        var exitsChapter =  await _entities.Where(x => x.ChapterName == name).FirstOrDefaultAsync();
        if (exitsChapter == null)
        {
            return true;
        }

        return false;
    }

    public async Task<ResponseDto> UpdateChapter(ChapterCreateDto dto)
    {
        var chapter = await _entities.FirstOrDefaultAsync(c => c.Id == dto.Id);

        if (chapter == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Chapter does not exist anymore!!!");
        }

        // Update the chapter using EntityFramework.Extended
        await _entities
            .Where(c => c.Id == dto.Id)
            .UpdateAsync(c => new Chapter
            {
                ChapterName = dto.ChapterName,
                ChapterLevel = dto.ChapterLevel,
                UpdatedAt = DateTime.Now
            });
        return new ResponseDto(HttpStatusCode.OK, "Chapter successfully updated.");
    }

    public async Task<ResponseDto> CreateChapter(Chapter chapter)
    {
        _context.Chapters.Add(chapter);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Chapter successfully created.");
    }

    public async Task<ChapterResponseDto> GetChapterByChapterName(string chapterName)
    {
        return await _entities.AsNoTracking()
            .Where(
                x => x.ChapterName == chapterName && x.IsDeleted == false).Select(x => new ChapterResponseDto()
            {
                Id = x.Id,
                ChapterName = x.ChapterName,
                ChapterLevel = x.ChapterLevel,
                SubjectId = x.SubjectId,
                CreatedAt = x.CreatedAt
                //update lesson after
            //     Lessons= x.Lessons.Select(c => new Lesson()
            //     {
            //     Id = c.Id,
            //     ChapterName = c.ChapterName,
            //     ChapterLevel = c.ChapterLevel
            // }).ToList()
            }).FirstOrDefaultAsync();
    }
    private IQueryable<Chapter> ApplyFilterSortAndSearch(IQueryable<Chapter> chapters, ChapterQueryFilter queryFilter)
    {
        chapters = chapters.Where(c => c.IsDeleted == false);

        if (!string.IsNullOrEmpty(queryFilter.Search))
        {
            chapters = chapters.Where(c => c.ChapterName.Contains(queryFilter.Search));
        }
        return chapters;
    }

    private IQueryable<Chapter> ApplySorting(IQueryable<Chapter> chapters, ChapterQueryFilter queryFilter)
    {
        chapters = queryFilter.Sort.ToLower() switch
        {
            "name" => queryFilter.SortDirection.ToLower() == "desc"
                ? chapters.OrderByDescending(c => c.ChapterName)
                : chapters.OrderBy(c => c.ChapterName),
            _ => queryFilter.SortDirection.ToLower() == "desc"
                ? chapters.OrderByDescending(c => c.CreatedAt).ThenBy(c => c.ChapterName)
                : chapters.OrderBy(c => c.CreatedAt).ThenBy(c => c.ChapterName),
        };
        return chapters;
    }
}