using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.VoteInterface;

public interface IVoteService
{
    Task<ResponseDto> DiscussionVoting(Guid guid);
    Task<ResponseDto> AnswerVoting(Guid guid);

}