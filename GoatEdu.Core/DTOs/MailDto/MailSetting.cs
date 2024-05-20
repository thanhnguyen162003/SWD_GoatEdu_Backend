namespace GoatEdu.Core.DTOs.MailDto;

public class MailSetting
{
    public string DisplayName { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public string Mail { get; set; } = null!;
    public string Password { get; set; } = null!;
}