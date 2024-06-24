using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs.UserDetailDto;

public class UserUpdateDto
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }

    public IFormFile? Image { get; set; }
    public string? password { get; set; }
}