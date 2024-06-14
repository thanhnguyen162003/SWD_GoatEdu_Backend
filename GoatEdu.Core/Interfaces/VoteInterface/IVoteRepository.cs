using Infrastructure;

namespace GoatEdu.Core.Interfaces.VoteInterface;

public interface IVoteRepository
{
    Task AddVote(Vote vote);
    void DeleteVote (Vote vote);
    Task<List<Guid?>> GetDiscussionVoteByUserId(Guid userId, List<Guid> discussionIds);
    Task<Vote?> GetDiscussionVote(Guid guid, Guid userId);
    Task<Vote?> GetAnswerVote(Guid guid, Guid userId);
}