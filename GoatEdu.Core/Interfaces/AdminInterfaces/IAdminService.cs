using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;

namespace GoatEdu.Core.Interfaces.AdminInterfaces;

public interface IAdminService
{
    Task<ResponseDto> CreateUser(CreateUserRequestDto user);
    Task<ResponseDto> SuppenseUser(Guid id);
}