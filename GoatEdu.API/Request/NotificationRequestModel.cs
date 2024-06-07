using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class NotificationRequestModel
{
    [Required]
    public string? NotificationName { get; set; }
    [Required]
    public string? NotificationMessage { get; set; }
    [Required]
    public Guid? UserId { get; set; }
}