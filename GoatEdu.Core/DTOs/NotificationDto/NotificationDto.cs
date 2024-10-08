namespace GoatEdu.Core.DTOs.NotificationDto;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string? NotificationName { get; set; }
    public string? NotificationMessage { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? CreateAt { get; set; }
}