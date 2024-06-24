using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.UserDetailInterfaces;

public interface IUserDetailRepository : IRepository<User>
{
    Task<ResponseDto> UpdateProfile(User user);
    Task<ResponseDto> GetSubcription();
    Task<Wallet> GetUserWallet();
    Task<ResponseDto> GetTranstractionWallet(Guid walletId);
    Task<ResponseDto> UpdateSubscription(User user);
    Task<ResponseDto> UpdateNewUser(Guid userId);
}