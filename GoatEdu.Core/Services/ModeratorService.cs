using System.Net;
using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;

namespace GoatEdu.Core.Services;

public class ModeratorService : IModeratorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    
    public ModeratorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    
    public async Task<ResponseDto> ApprovedDiscussion(List<Guid> guids)
    {
        await _unitOfWork.ModeratorRepository.ApprovedDiscussions(guids);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new ResponseDto(HttpStatusCode.OK, "Approve Successfully!") : new ResponseDto(HttpStatusCode.BadRequest, "Approve Failed!");
    }
}