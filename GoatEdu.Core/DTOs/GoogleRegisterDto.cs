namespace GoatEdu.Core.DTOs;

public class GoogleRegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    public string? Picture { get; set; }
}