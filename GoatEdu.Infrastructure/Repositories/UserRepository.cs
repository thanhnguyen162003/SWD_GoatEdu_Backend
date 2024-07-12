using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly GoatEduContext _context;
    
    public UserRepository(GoatEduContext context): base(context)
    {
        _context = context;
    }
    
    public async Task<User> GetUserByUserId(Guid userId)
    {
        return await _entities.Where(x => x.Id == userId && x.IsDeleted == false).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByGoogle(string email)
    {
        return await _entities.Where(
            x => x.Email == email && x.IsDeleted == false).Include(x => x.Role).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _entities
            .Where(x => (x.Username == username || x.Email == username) && x.IsDeleted == false)
            .Include(x => x.Role)
            .FirstOrDefaultAsync();
    }
    public async Task<User> GetUserByUsernameWithEmailCheckRegister(string username, string email)
    {
        return await _entities.Where(
            x => x.Username == username && x.IsDeleted == false || x.Email == email && x.IsDeleted == false
        ).FirstOrDefaultAsync();
    }
    public async Task<User> GetUserByUsernameNotGoogle(string username)
    {
        return await _entities
            .Where(x => (x.Username == username || x.Email == username) && x.IsDeleted == false && x.Provider != UserEnum.GOOGLE)
            .Include(x => x.Role)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<UserMinimalDto>> GetUsersInRole(Guid roleId)
    {
        return await _context.Users
            .Where(ur => ur.Role.Id == roleId)
            .Select(ur => new UserMinimalDto
            {
                Id = ur.Id,
                Username = ur.Username,
                Email = ur.Email,
                PhoneNumber = ur.PhoneNumber,
                Fullname = ur.Fullname,
                RoleName = ur.Role.RoleName,
                IsConfirmMail = ur.EmailVerify,
                IsDeleted = ur.IsDeleted
            })
            .ToListAsync();
    }

    public async Task<bool> IdExistAsync(Guid userId)
    {
        return await _entities.AnyAsync(x => x.Id == userId);
    }

    public async Task<User> AddUser(User user)
    {
        var result = await _entities.AddAsync(user);
        await _context.SaveChangesAsync(); // Await SaveChangesAsync
        return user;
    }
    
}