using GoatEdu.Core.DTOs;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.UserInterfaces;

public interface IUserService
{
    Task<User> GetUserByUsername(string username);
    Task<ResponseDto> GetLoginByCredentials(LoginDtoRequest login);
    Task<ResponseDto> Register(RegisterDto registerUser);

    Task<ResponseDto> LoginByGoogle(GoogleDto dto);

    Task<ResponseDto> RegisterByGoogle(GoogleDto dto);

}