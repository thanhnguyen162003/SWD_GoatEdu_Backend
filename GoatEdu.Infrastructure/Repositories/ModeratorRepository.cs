using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModeratorRepository : IModeratorRepository
{
    private readonly GoatEduContext _context;
    
    public ModeratorRepository(GoatEduContext context)
    {
        _context = context;
    }
    
    public async Task<Guid?> ApprovedDiscussions(Guid discussionId)
    {
        var discussion = await _context.Discussions
            .Where(x => x.Id == discussionId)
            .FirstOrDefaultAsync();

        if (discussion == null)
        {
            return null;
        }
        
        discussion.Status = StatusConstraint.APPROVED;
        return discussion.UserId;
    }

    public async Task<SubjectDto> GetSubjectBySubjectId(Guid id)
    {
        return await _context.Subjects.AsNoTracking().Where(
            x => x.Id == id && x.IsDeleted == false).Select(x => new SubjectDto()
        {
            Id = x.Id,
            SubjectName = x.SubjectName,
            SubjectCode = x.SubjectCode,
            Class = x.Class,
            Information = x.Information,
            Image = x.Image,
            CreatedAt = x.CreatedAt,
            Chapters = x.Chapters
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.ChapterLevel)
                .Select(c => new ChapterSubjectDto()
                {
                    Id = c.Id,
                    ChapterName = c.ChapterName,
                    SubjectId = c.SubjectId,
                    CreatedAt = c.CreatedAt,
                    ChapterLevel = c.ChapterLevel
                }).ToList(),
            NumberOfChapters = x.Chapters.Count // New field for number of chapters
        }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectByClass(string classes, SubjectQueryFilter queryFilter)
    {
        var subjects = _context.Subjects.AsNoTracking().Where(x=>x.Class.ToLower().Equals(classes.ToLower())).Include(x => x.Chapters.Where(y=>y.IsDeleted == false)).AsQueryable();
        subjects = ApplyFilterSortAndSearch(subjects, queryFilter);
        subjects = ApplySorting(subjects, queryFilter);
        return await subjects.ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetAllSubjects(SubjectQueryFilter queryFilter)
    {
        var subjects = _context.Subjects
            .Include(x => x.Chapters.Where(chapter => chapter.IsDeleted == false))
            .AsQueryable();
        subjects = ApplyFilterSortAndSearch(subjects, queryFilter);
        subjects = ApplySorting(subjects, queryFilter);
        return await subjects.ToListAsync();
    }

    private IQueryable<Subject> ApplyFilterSortAndSearch(IQueryable<Subject> subjects, SubjectQueryFilter queryFilter)
    {
        subjects = subjects.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.search))
        {
            subjects = subjects.Where(x => x.SubjectName.ToLower().Contains(queryFilter.search.ToLower()));
        }
        return subjects;
    }
    
    private IQueryable<Subject> ApplySorting(IQueryable<Subject> subjects, SubjectQueryFilter queryFilter)
    {
        subjects = queryFilter.sort.ToLower() switch
        {
            "name" => queryFilter.sort_direction.ToLower() == "desc"
                ? subjects.OrderByDescending(x => x.SubjectName)
                : subjects.OrderBy(x => x.SubjectName),
            _ => queryFilter.sort_direction.ToLower() == "desc"
                ? subjects.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.SubjectName)
                : subjects.OrderBy(x => x.CreatedAt).ThenBy(x => x.SubjectName),
        };
        return subjects;
    }
}