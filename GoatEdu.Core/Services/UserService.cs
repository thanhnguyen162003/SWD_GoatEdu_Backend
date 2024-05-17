using System.Data;
using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JWTGenerator _tokenGenerator;

    public UserService(IUserRepository userRepository, JWTGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }

    public async Task<ResponseDto> GetLoginByCredentials(LoginDtoRequest login)
    {
        var user = await _userRepository.GetUserByUsername(login.Username);
        // var emailUser = await _userRepository.GetUserByEmail(login.Username);
        if (user == null)
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

        var token = _tokenGenerator.GenerateToken(login);
        return new ResponseDto(HttpStatusCode.OK, "Login successfully!", token);
    }

    public async Task<ResponseDto> Register(RegisterDto registerUser)
    {
        //check email exist???
        var isUserExits = await _userRepository.GetUserByUsername(registerUser.Username);
        if (isUserExits != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Username has been taken!");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
        User user = new User()
        {
            Username = registerUser.Username,
            Password = hashedPassword,
            RoleId = registerUser.RoleId,
            PhoneNumber = null,
            Email = registerUser.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false
        };
        var result = await _userRepository.AddUser(user);
        //send confirm email here!!!
        
        if (result == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Somethings has error!");
        }

        return new ResponseDto(HttpStatusCode.OK, "Register Successful, please check your email!");
    }
}