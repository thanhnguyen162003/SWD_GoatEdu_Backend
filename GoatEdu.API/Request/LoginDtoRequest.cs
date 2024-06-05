namespace GoatEdu.API.Request;

public class LoginDtoRequest
{
   public string? Username { get; set; }
   public string? Password { get; set; }
   public string? Email { get; set; }
   public string? Picture { get; set; }
}