using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
{
    private readonly GoatEduContext _context;
    public SubjectRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }


    public Task<ICollection<SubjectResponseDto>> GetAllSubjects()
    {
        throw new NotImplementedException();
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