using GoatEdu.Core.DTOs;
using Infrastructure.Models;

namespace GoatEdu.Core.Interfaces.UserInterfaces;

public interface IUserService
{
    Task<User> GetUserByUsername(string username);
    IEnumerable<User> GetUserByName(string name);
    Task<ResponseDto> GetLoginByCredentials(LoginDtoRequest login);
    Task<ResponseDto> Register(RegisterDto registerUser);
    
}