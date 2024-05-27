namespace GoatEdu.Core.DTOs;

public class DiscussionRequestDto
{
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionImage { get; set; }
    public Guid? SubjectId { get; set; }
    public bool? IsSolved { get; set; } = false;
}