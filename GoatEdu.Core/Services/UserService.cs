using System.Data;
using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Models;
using Infrastructure;
using MailKit;

namespace GoatEdu.Core.Services;

public class UserService : IUserService
{
    private readonly JWTGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly Interfaces.MailInterfaces.IMailService _mailService;

    public UserService(IUnitOfWork unitOfWork, JWTGenerator tokenGenerator, IMapper mapper, Interfaces.MailInterfaces.IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
        _mailService = mailService;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _unitOfWork.UserRepository.GetUserByUsername(username);
    }
    
    public async Task<ResponseDto> LoginByGoogle(LoginGoogleDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByGoogle(dto.Email);
        if (user == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "Email not existed!");
        }
        if (user.IsDeleted == true)
        {
            return new ResponseDto(HttpStatusCode.Forbidden, "Your account is suspense!");
        }

        LoginDtoRequest loginDtoRequest = new LoginDtoRequest()
        {
            Email = dto.Email,
            Username = user.Fullname
        };
        var token = _tokenGenerator.GenerateToken(loginDtoRequest);
        return new ResponseDto(HttpStatusCode.OK, "Login successfully!", token);
    }

    public async Task<ResponseDto> RegisterByGoogle(GoogleRegisterDto dto)
    {
        var isUserExits = await _unitOfWork.UserRepository.GetUserByGoogle(dto.Email);
        if (isUserExits != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Account has been linked!, please login.");
        }
        User user = new User()
        {
            Fullname = dto.Name,
            RoleId = dto.RoleId,
            PhoneNumber = null,
            Email = dto.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false,
            EmailVerify = true,
            Provider = UserEnum.GOOGLE
        };
        var result = await _unitOfWork.UserRepository.AddUser(user);
        if (result == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Somethings has error!");
        }

        return new ResponseDto(HttpStatusCode.OK, "Register Successful, please login again!");
    }
    

    public async Task<ResponseDto> GetLoginByCredentials(LoginCredentialDto login)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameNotGoogle(login.Username);
        // var emailUser = await _userRepository.GetUserByEmail(login.Username);
        
        if (user == null)
        {
            return new ResponseDto(HttpStatusCode.NotFound, "UserName or Email not existed or you have register with Google");
        }
        if (user.EmailVerify == false)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Please CONFIRM YOUR FAKING EMAIL !!!");
        }

        if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Wrong password!");
        }
        // need add verify email to continute
        if (user.IsDeleted == true)
        {
            return new ResponseDto(HttpStatusCode.Forbidden, "Your account is suspense!");
        }

        LoginDtoRequest loginDtoRequest = new LoginDtoRequest()
        {
            Username = login.Username,
            Password = login.Password
        };
       
        var token = _tokenGenerator.GenerateToken(loginDtoRequest);
        var loginResponse = _mapper.Map<LoginResponseDto>(user);
        loginResponse.Token = token;
        return new ResponseDto(HttpStatusCode.OK, "Login successfully!", loginResponse);
    }

    public async Task<ResponseDto> Register(RegisterDto registerUser)
    {
        //check email exist???
        var isUserExits = await _unitOfWork.UserRepository.GetUserByUsernameWithEmailCheckRegister(registerUser.Username, registerUser.Email);
        if (isUserExits != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Account has been exits!");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
        string hashedUsername = BCrypt.Net.BCrypt.HashPassword(registerUser.Username);
        User user = new User()
        {
            Username = registerUser.Username,
            Password = hashedPassword,
            RoleId = registerUser.RoleId,
            Fullname = registerUser.FullName,
            PhoneNumber = null,
            Email = registerUser.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false,
            EmailVerify = false,
            Provider = UserEnum.CREDENTIAL
        };
        var result = await _unitOfWork.UserRepository.AddUser(user);
        UserMail userMail = new UserMail()
        {
            Username = hashedUsername,
            Email = registerUser.Email,
            Fullname = registerUser.FullName,
            Password = hashedPassword,
            Id = user.Id
        };
        //send confirm email here!!!
        await _mailService.SendUsingTemplateFromFile("Responses/VerifyToken.cshtml","Dit me confirm cho tao", userMail);
        if (result == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Somethings has error!");
        }

        return new ResponseDto(HttpStatusCode.OK, "Register Successful, please check your email!");
    }
}