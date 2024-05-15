using System.Data;
using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure.Models;

namespace GoatEdu.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JWTGenerator _tokenGenerator;
   
    public UserService(IUserRepository userRepository,JWTGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<User> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }

    public IEnumerable<User> GetUserByName(string name)
    {
        return _userRepository.GetUserByName(name);
    }

    public async Task<ResponseDto> GetLoginByCredentials(LoginDtoRequest login)
    {
        
        var user = await _userRepository.GetUserByUsername(login.Username);
        if(user == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "UserName or Email not existed!");
        }
        if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Wrong password!");
        }
        if (user.IsDeleted == true)
        {
            return new ResponseDto(HttpStatusCode.Forbidden, "Your account is suspense!");
        }
        var token =  _tokenGenerator.GenerateToken(login);
        return new ResponseDto(HttpStatusCode.OK, "Login successfully!", token );
        
    }

    public Task<ResponseDto> Register(ResponseDto registerUser)
    {
        throw new NotImplementedException();
    }

 
}