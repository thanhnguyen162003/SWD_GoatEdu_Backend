using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserDetailRepository : BaseRepository<User>, IUserDetailRepository
{
    private readonly GoatEduContext _context;
    
    public UserDetailRepository(GoatEduContext context): base(context)
    {
        _context = context;
    }

    public async Task<ResponseDto> UpdateProfile(User user)
    {
        var existingUser = await _entities.FindAsync(user.Id);
        if (existingUser == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "User not found.");
        }

        existingUser.Fullname = user.Fullname ?? existingUser.Fullname;
        existingUser.Image = user.Image ?? existingUser.Image;
        existingUser.PhoneNumber = user.PhoneNumber ?? existingUser.PhoneNumber;
        existingUser.UpdatedAt = DateTime.Now;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        return new ResponseDto(HttpStatusCode.OK, "User successfully updated.", existingUser.Image);
    }
    public async Task<ResponseDto> UpdateNewUser(Guid userId)
    {
        var existingUser = await _entities.FindAsync(userId);
        if (existingUser == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "User not found.");
        }

        existingUser.IsNewUser = false;
       

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        return new ResponseDto(HttpStatusCode.OK, "User successfully updated.");
    }
    public async Task<ResponseDto> UpdatePassword(Guid userId, string hashedPassword)
    {
        var existingUser = await _entities.FindAsync(userId);
        if (existingUser == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "User not found.");
        }

        existingUser.Password = hashedPassword;
       

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        return new ResponseDto(HttpStatusCode.OK, "User password updated.");
    }
    public async Task<ResponseDto> UpdateSubscription(User user)
    {
        var existingUser = await _entities.FindAsync(user.Id);
        if (existingUser == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "User not found.");
        }

        existingUser.SubscriptionEnd = user.SubscriptionEnd;
        existingUser.Subscription = user.Subscription;
        existingUser.UpdatedAt = DateTime.Now;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        return new ResponseDto(HttpStatusCode.OK, "User successfully updated.");
    }

    public Task<ResponseDto> GetSubcription()
    {
        throw new NotImplementedException();
    }

    public Task<Wallet> GetUserWallet()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> GetTranstractionWallet(Guid walletId)
    {
        throw new NotImplementedException();
    }

    
}