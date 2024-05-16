using GoatEdu.Core.DTOs;
using Infrastructure.Models;

namespace GoatEdu.Core.Interfaces.UserInterfaces;

public interface IUserService
{
    Task<User> GetUserByUsername(string username);
    Task<ResponseDto> GetLoginByCredentials(LoginDtoRequest login);
    Task<ResponseDto> Register(RegisterDto registerUser);
    
}