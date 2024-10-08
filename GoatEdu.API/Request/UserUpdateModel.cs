using Microsoft.AspNetCore.Http;

namespace GoatEdu.API.Request;

public class UserUpdateModel
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }

    public IFormFile? Image { get; set; }
    public string password { get; set; }
}