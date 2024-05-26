namespace GoatEdu.Core.DTOs;

public class UserAndSubject
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public Guid SubjectId { get; set; }
    public string? SubjectName { get; set; }
}