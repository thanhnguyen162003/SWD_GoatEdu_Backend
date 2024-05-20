namespace GoatEdu.Core.DTOs.MailDto;

public class ConfirmMailDto
{
    public string? userId { get; set; }
    public string? token { get; set; }
    public Guid id { get; set; }
}