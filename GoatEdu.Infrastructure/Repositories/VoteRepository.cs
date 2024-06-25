using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.VoteInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly GoatEduContext _context;

    public VoteRepository(GoatEduContext context)
    {
        _context = context;
    }

    public async Task AddVote(Vote vote)
    {
        await _context.Votes.AddAsync(vote);    
    }

    public void DeleteVote(Vote vote)
    {
        _context.Votes.Remove(vote);
    }

    public async Task<List<Guid?>> GetDiscussionVoteByUserId(Guid userId, IEnumerable<Guid> discussionIds)
    {
        return await _context.Votes
            .Where(x => x.UserId == userId && discussionIds.Contains((Guid)x.DiscussionId))
            .Select(x => x.DiscussionId)
            .ToListAsync();
    }

    public async Task<List<Guid?>> GetAnswerVoteByUserId(Guid userId, IEnumerable<Guid> answerIds)
    {
        return await _context.Votes
            .Where(x => x.UserId == userId && answerIds.Contains((Guid)x.AnswerId))
            .Select(x => x.AnswerId)
            .ToListAsync();
    }

    public async Task<Vote?> GetDiscussionVote(Guid guid, Guid userId)
    {
        return await _context.Votes.FirstOrDefaultAsync(x => x.UserId == userId && x.DiscussionId == guid);
    }

    public async Task<Vote?> GetAnswerVote(Guid guid, Guid userId)
    {
        return await _context.Votes.FirstOrDefaultAsync(x => x.UserId == userId && x.AnswerId == guid);
    }
}