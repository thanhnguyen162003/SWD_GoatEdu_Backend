using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.UserDetailDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.UserDetailInterfaces;

public interface IUserDetailService
{
    Task<ResponseDto> UpdateProfile(UserUpdateDto user);
    Task<ResponseDto> GetSubcription();
    Task<Wallet> GetUserWallet();
    Task<ResponseDto> GetTranstractionWallet(Guid walletId);
    Task<ResponseDto> UpdateNewUser();

}