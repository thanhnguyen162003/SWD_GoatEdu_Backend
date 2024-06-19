using Infrastructure;

namespace GoatEdu.Core.Interfaces.VoteInterface;

public interface IVoteRepository
{
    Task AddVote(Vote vote);
    void DeleteVote (Vote vote);
    Task<List<Guid?>> GetDiscussionVoteByUserId(Guid userId, IEnumerable<Guid> discussionIds);
    Task<Vote?> GetDiscussionVote(Guid guid, Guid userId);
    Task<Vote?> GetAnswerVote(Guid guid, Guid userId);
}