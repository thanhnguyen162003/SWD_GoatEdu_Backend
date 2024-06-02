using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.ModeratorInterfaces;

public interface IModeratorService
{
    Task<ResponseDto> ApprovedDiscussion(List<Guid> guids);

}