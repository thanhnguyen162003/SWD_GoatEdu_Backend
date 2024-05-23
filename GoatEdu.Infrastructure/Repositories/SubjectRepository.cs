using System.Net;
using EntityFramework.Extensions;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.Models;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
{
    private readonly GoatEduContext _context;

    public SubjectRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<Subject>> GetAllSubjects(SubjectQueryFilter queryFilter)
    {
        // return await _entities
        //     .AsNoTracking()
        //     .Where(x => x.IsDeleted == false)
        //     .Select(x => new SubjectResponseDto()
        //     {
        //         Id = x.Id,
        //         SubjectName = x.SubjectName,
        //         Image = x.Image,
        //         SubjectCode = x.SubjectCode,
        //         Information = x.Information,
        //         Class = x.Class,
        //         Chapters = x.Chapters.Select(c => new ChapterSubjectDto()
        //         {
        //             Id = c.Id,
        //             ChapterName = c.ChapterName,
        //             ChapterLevel = c.ChapterLevel
        //             // Include other Chapter properties you need here
        //         }).ToList(),
        //         CreatedAt = x.CreatedAt
        //     })
        //     .ToListAsync();
        var subjects = _entities.AsQueryable();
        subjects = ApplyFilterSortAndSearch(subjects, queryFilter);
        subjects = ApplySorting(subjects, queryFilter);
        return await subjects.ToListAsync();
    }

    public async Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id)
    {
        return await _entities.AsNoTracking().Where(
            x => x.Id == id && x.IsDeleted == false).Select(x => new SubjectResponseDto()
        {
            Id = x.Id,
            SubjectName = x.SubjectName,
            SubjectCode = x.SubjectCode,
            Class = x.Class,
            Information = x.Information,
            Image = x.Image,
            CreatedAt = x.CreatedAt,
            Chapters = x.Chapters.Select(c => new ChapterSubjectDto()
            {
                Id = c.Id,
                ChapterName = c.ChapterName,
                ChapterLevel = c.ChapterLevel
            }).ToList()
        }).FirstOrDefaultAsync();
    }

    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        var subject = await _entities.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (subject == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Subject does not exist anymore!!!");
        }

        // Set IsDeleted to true
        subject.IsDeleted = true;

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return a successful response
        return new ResponseDto(HttpStatusCode.OK, "Subject successfully deleted.");
    }

    public async Task<ResponseDto> UpdateSubject(SubjectCreateDto dto)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (subject == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Subject does not exist anymore!!!");
        }

        // Update the subject using EntityFramework.Extended
        await _entities
            .Where(x => x.Id == dto.Id)
            .UpdateAsync(s => new Subject
            {
                SubjectName = dto.SubjectName,
                SubjectCode = dto.SubjectCode,
                Information = dto.Information,
                Image = dto.Image,
                Class = dto.Class,
                UpdatedAt = DateTime.Now
            });
        return new ResponseDto(HttpStatusCode.OK, "Subject successfully updated.");
    }

    public async Task<ResponseDto> CreateSubject(Subject subject)
    {
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        return new ResponseDto(HttpStatusCode.OK, "Create Success");
    }

    public async Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName)
    {
        return await _entities.AsNoTracking()
            .Where(
            x => x.SubjectName == subjectName && x.IsDeleted == false).Select(x => new SubjectResponseDto()
        {
            Id = x.Id,
            SubjectName = x.SubjectName,
            SubjectCode = x.SubjectCode,
            Class = x.Class,
            Information = x.Information,
            Image = x.Image,
            CreatedAt = x.CreatedAt,
            Chapters = x.Chapters.Select(c => new ChapterSubjectDto()
            {
                Id = c.Id,
                ChapterName = c.ChapterName,
                ChapterLevel = c.ChapterLevel
            }).ToList()
        }).FirstOrDefaultAsync();
    }
    private IQueryable<Subject> ApplyFilterSortAndSearch(IQueryable<Subject> subjects, SubjectQueryFilter queryFilter)
    {
        subjects = subjects.Where(x => x.IsDeleted == false);
        
        if (!string.IsNullOrEmpty(queryFilter.Search))
        {
            subjects = subjects.Where(x => x.SubjectName.Contains(queryFilter.Search));
        }
        return subjects;
    }
    
    private IQueryable<Subject> ApplySorting(IQueryable<Subject> subjects, SubjectQueryFilter queryFilter)
    {
        subjects = queryFilter.Sort.ToLower() switch
        {
            "name" => queryFilter.SortDirection.ToLower() == "desc"
                ? subjects.OrderByDescending(x => x.SubjectName)
                : subjects.OrderBy(x => x.SubjectName),
            _ => queryFilter.SortDirection.ToLower() == "desc"
                ? subjects.OrderByDescending(x => x.CreatedAt).ThenBy(x => x.SubjectName)
                : subjects.OrderBy(x => x.CreatedAt).ThenBy(x => x.SubjectName),
        };
        return subjects;
    }
}