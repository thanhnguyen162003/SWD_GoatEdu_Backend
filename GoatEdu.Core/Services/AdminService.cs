using System.Net;
using AutoMapper;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.AdminInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseDto> CreateUser(CreateUserRequestDto user)
    {
        var isUserExits = await _unitOfWork.UserRepository.GetUserByUsernameWithEmailCheckRegister(user.Username, user.Email);
        if (isUserExits != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Account has been exits!");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        User userByAdmin = new User()
        {
            Username = user.Username,
            Password = hashedPassword,
            RoleId = user.RoleId,
            Fullname = user.FullName,
            PhoneNumber = null,
            Email = user.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false,
            EmailVerify = true,
            Provider = UserEnum.CREDENTIAL
        };
        var result = await _unitOfWork.UserRepository.AddUser(userByAdmin);
      
        if (result == null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Somethings has error!");
        }
        var userMapper = _mapper.Map<CreateUserResponse>(userByAdmin);
        return new ResponseDto(HttpStatusCode.OK, "Create User Success;",userMapper);
    }

    public async Task<ResponseDto> SuppenseUser(Guid id)
    {
        return await _unitOfWork.AdminRepository.SuppenseUser(id);
    }
}