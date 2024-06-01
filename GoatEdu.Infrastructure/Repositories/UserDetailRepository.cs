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

        existingUser.Fullname = user.Fullname;
        existingUser.Image = user.Image;
        existingUser.PhoneNumber = user.PhoneNumber;
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