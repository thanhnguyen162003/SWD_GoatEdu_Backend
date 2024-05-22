using System.Net;
using EntityFramework.Extensions;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
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

    public async Task<ICollection<SubjectResponseDto>> GetAllSubjects()
    {
        return await _entities
            .AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .Select(x => new SubjectResponseDto()
            {
                Id = x.Id,
                SubjectName = x.SubjectName,
                Image = x.Image,
                SubjectCode = x.SubjectCode,
                Information = x.Information,
                Class = x.Class,
                Chapters = x.Chapters.Select(c => new ChapterSubjectDto()
                {
                    Id = c.Id,
                    ChapterName = c.ChapterName,
                    ChapterLevel = c.ChapterLevel
                    // Include other Chapter properties you need here
                }).ToList(),
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
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

    public Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName)
    {
        throw new NotImplementedException();
    }
}