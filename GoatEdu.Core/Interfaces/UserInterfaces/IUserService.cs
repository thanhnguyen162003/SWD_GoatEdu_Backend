using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.UserInterfaces;

public interface IUserService
{
    Task<User> GetUserByUsername(string username);
    Task<ResponseDto> GetLoginByCredentials(LoginCredentialDto login);
    Task<ResponseDto> Register(RegisterDto registerUser);

    Task<ResponseDto> LoginByGoogle(string email);

    Task<ResponseDto> RegisterByGoogle(GoogleRegisterDto dto);

}