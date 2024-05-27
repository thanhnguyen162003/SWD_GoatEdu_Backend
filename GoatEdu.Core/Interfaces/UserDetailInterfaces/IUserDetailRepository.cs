using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.UserDetailInterfaces;

public interface IUserDetailRepository
{
    Task<ResponseDto> UpdateProfile(User user);
    Task<ResponseDto> GetSubcription();
    Task<Wallet> GetUserWallet();
    Task<ResponseDto> GetTranstractionWallet(Guid walletId);

}