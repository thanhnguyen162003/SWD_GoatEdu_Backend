using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;

namespace GoatEdu.Core.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubjectService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ICollection<SubjectResponseDto>> GetAllSubjects()
    {
        var result = await _unitOfWork.SubjectRepository.GetAllSubjects();
        return result;
    }

    public async Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id)
    {
        return await _unitOfWork.SubjectRepository.GetSubjectBySubjectId(id);
    }

    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        return await _unitOfWork.SubjectRepository.DeleteSubject(id);
    }

    public async Task<ResponseDto> UpdateSubject(SubjectCreateDto dto)
    {
        return await _unitOfWork.SubjectRepository.UpdateSubject(dto);
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