using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces.VoteInterface;

public interface IVoteService
{
    Task<ResponseDto> DiscussionVoting(Guid userId, Guid discussionid);
    Task<ResponseDto> AnswerVoting(Guid userId, Guid answerGuid);

}