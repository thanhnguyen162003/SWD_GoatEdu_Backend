using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class CreateUserRequestModel
{
    [Required(ErrorMessage = "User Name is required")]
    [RegularExpression(@"^[a-z0-9]+$", ErrorMessage = "Username must contain only lowercase letters and digits.")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Fullname is required")]
    public string? FullName { get; set; }
    
    [Required(ErrorMessage = "Phone Number is required")]
    public string? PhoneNumber { get; set; }
}