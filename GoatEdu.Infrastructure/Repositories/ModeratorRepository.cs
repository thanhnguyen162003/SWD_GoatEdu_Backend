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
    
    public async Task ApprovedDiscussions(List<Guid> guids)
    {
        await _context.Discussions.Where(x => guids.Any(id => id == x.Id)).ForEachAsync(a => a.Status = DiscussionStatus.Approved.ToString());
    }
}