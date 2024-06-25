using Infrastructure;

namespace GoatEdu.Core.Interfaces.VoteInterface;

public interface IVoteRepository
{
    Task AddVote(Vote vote);
    void DeleteVote (Vote vote);
    Task<List<Guid?>> GetDiscussionVoteByUserId(Guid userId, IEnumerable<Guid> discussionIds);
    Task<List<Guid?>> GetAnswerVoteByUserId(Guid userId, IEnumerable<Guid> answerIds);
    Task<Vote?> GetDiscussionVote(Guid guid, Guid userId);
    Task<Vote?> GetAnswerVote(Guid guid, Guid userId);
}