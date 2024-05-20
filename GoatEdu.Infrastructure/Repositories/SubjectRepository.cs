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

    public Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> DeleteSubject(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> UpdateSubject(SubjectCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> CreateSubject(SubjectCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName)
    {
        throw new NotImplementedException();
    }
}