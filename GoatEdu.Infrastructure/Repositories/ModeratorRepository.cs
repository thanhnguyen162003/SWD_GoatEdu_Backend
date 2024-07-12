using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModeratorRepository : IModeratorRepository
{
    private readonly GoatEduContext _context;
    
    public ModeratorRepository(GoatEduContext context)
    {
        _context = context;
    }
    
    public async Task<Guid?> ApprovedDiscussions(Guid discussionId)
    {
        var discussion = await _context.Discussions
            .Where(x => x.Id == discussionId)
            .FirstOrDefaultAsync();

        if (discussion == null)
        {
            return null;
        }
        
        discussion.Status = StatusConstraint.APPROVED;
        return discussion.UserId;
    }
}