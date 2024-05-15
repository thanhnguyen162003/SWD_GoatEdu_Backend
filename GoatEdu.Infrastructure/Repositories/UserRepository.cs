using System.Data;
using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly GoatEduContext _context;
    
    public UserRepository(GoatEduContext context): base(context)
    {
        _context = context;
    }

    public IEnumerable<User> GetUserByName(string name)
    {
        throw new NotImplementedException();
    }
    
    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.Where(x => x.Username == username && x.IsDeleted == false).FirstOrDefaultAsync();
    }
    
    
}