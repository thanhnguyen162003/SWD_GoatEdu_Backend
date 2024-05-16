using System.Data;
using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
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
        return await _entities.Where(
            x => x.Username == username && x.IsDeleted == false || x.Email == username && x.IsDeleted == false
            ).FirstOrDefaultAsync();
    }
    
    public async Task<User> AddUser(User user)
    {
        var result = await _entities.AddAsync(user);
        await _context.SaveChangesAsync(); // Await SaveChangesAsync
        return user;
    }
    
}