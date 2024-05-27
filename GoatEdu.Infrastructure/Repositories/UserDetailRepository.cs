using System.Net;
using EntityFramework.Extensions;
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
        await _entities
            .Where(x => x.Id == user.Id)
            .UpdateAsync(s => new User()
            {
                Fullname = user.Fullname,
                Image = user.Image,
                PhoneNumber = user.PhoneNumber,
                UpdatedAt = DateTime.Now
            });
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