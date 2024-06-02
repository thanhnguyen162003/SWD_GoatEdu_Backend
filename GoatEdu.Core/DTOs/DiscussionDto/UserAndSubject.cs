namespace GoatEdu.Core.DTOs;

public class UserAndSubject
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? UserImage { get; set; }
    public Guid SubjectId { get; set; }
    public string? SubjectName { get; set; }
}