using System.Net;
using AutoMapper;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.AdminInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;
using Microsoft.Extensions.Options;

namespace GoatEdu.Core.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly PaginationOptions _paginationOptions;

    public AdminService(IUnitOfWork unitOfWork, IMapper mapper,IOptions<PaginationOptions> paginationOptions)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationOptions = paginationOptions.Value;
    }
    public async Task<ResponseDto> CreateUser(CreateUserDto user)
    {
        var isUserExits = await _unitOfWork.UserRepository.GetUserByUsernameWithEmailCheckRegister(user.Username, user.Email);
        if (isUserExits != null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Account has been exits!");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(DefaultData.PASSWORD_DEFAULT);
        User userByAdmin = new User()
        {
            Username = user.Username,
            Password = hashedPassword,
            RoleId = string.IsNullOrEmpty(DefaultData.ROLE_DEFAULT) ? Guid.Empty : new Guid(DefaultData.ROLE_DEFAULT),
            Fullname = user.FullName,
            PhoneNumber = user.PhoneNumber,
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
        var userMapper = _mapper.Map<CreateUserDto>(userByAdmin);
        return new ResponseDto(HttpStatusCode.OK, "Create User Success;",userMapper);
    }

    public async Task<ResponseDto> SuppenseUser(Guid id)
    {
        return await _unitOfWork.AdminRepository.SuppenseUser(id);
    }

    public async Task<PaginatedResponse<UserMinimalDto>> GetUsers(UserQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var listUsers = await _unitOfWork.AdminRepository.GetUsers(queryFilter);
    
        if (!listUsers.Any())
        {
            var emptyPagedList = new PagedList<UserMinimalDto>(new List<UserMinimalDto>(), 0, queryFilter.page_number, queryFilter.page_size);
            return new PaginatedResponse<UserMinimalDto>(emptyPagedList);
        }

        var mappedList = _mapper.Map<List<UserMinimalDto>>(listUsers);
        var pagedList = PagedList<UserMinimalDto>.Create(mappedList, queryFilter.page_number, queryFilter.page_size);

        return new PaginatedResponse<UserMinimalDto>(pagedList);
    }

    public async Task<PaginatedResponse<UserMinimalDto>> GetUserUsed(UserQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var listUsers = await _unitOfWork.AdminRepository.GetUserUsed(queryFilter);

        if (!listUsers.Any())
        {
            var emptyPagedList = new PagedList<UserMinimalDto>(new List<UserMinimalDto>(), 0, queryFilter.page_number, queryFilter.page_size);
            return new PaginatedResponse<UserMinimalDto>(emptyPagedList);
        }

        var mappedList = _mapper.Map<List<UserMinimalDto>>(listUsers);
        var pagedList = PagedList<UserMinimalDto>.Create(mappedList, queryFilter.page_number, queryFilter.page_size);

        return new PaginatedResponse<UserMinimalDto>(pagedList);
    }

    public async Task<PaginatedResponse<UserMinimalDto>> GetModerator(UserQueryFilter queryFilter)
    {
        queryFilter.page_number = queryFilter.page_number == 0 ? _paginationOptions.DefaultPageNumber : queryFilter.page_number;
        queryFilter.page_size = queryFilter.page_size == 0 ? _paginationOptions.DefaultPageSize : queryFilter.page_size;

        var listUsers = await _unitOfWork.AdminRepository.GetModerator(queryFilter);

        if (!listUsers.Any())
        {
            var emptyPagedList = new PagedList<UserMinimalDto>(new List<UserMinimalDto>(), 0, queryFilter.page_number, queryFilter.page_size);
            return new PaginatedResponse<UserMinimalDto>(emptyPagedList);
        }

        var mappedList = _mapper.Map<List<UserMinimalDto>>(listUsers);
        var pagedList = PagedList<UserMinimalDto>.Create(mappedList, queryFilter.page_number, queryFilter.page_size);

        return new PaginatedResponse<UserMinimalDto>(pagedList);
    }
}