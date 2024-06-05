namespace GoatEdu.API.Request;

public class NotificationRequestDto
{
    public string? NotificationName { get; set; }
    public string? NotificationMessage { get; set; }
    public Guid? UserId { get; set; }
}