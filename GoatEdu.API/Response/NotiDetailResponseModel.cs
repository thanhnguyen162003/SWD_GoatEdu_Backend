namespace GoatEdu.API.Response;

public class NotiDetailResponseModel
{
    public Guid Id { get; set; }
    public string? NotificationName { get; set; }
    public string? NotificationMessage { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? CreateAt { get; set; }
}