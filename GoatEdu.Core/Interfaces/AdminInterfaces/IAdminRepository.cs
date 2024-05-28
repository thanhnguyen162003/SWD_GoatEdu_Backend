using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.UserDetailDto;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.AdminInterfaces;

public interface IAdminRepository
{
    Task<ResponseDto> SuppenseUser(Guid id);
}