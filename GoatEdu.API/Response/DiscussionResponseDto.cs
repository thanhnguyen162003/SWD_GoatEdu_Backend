namespace GoatEdu.API.Response;

public class DiscussionResponseDto
{
    public Guid Id { get; set; }
    public string? DiscussionName { get; set; }
    public string? UserName { get; set; }
    public int? DiscussionVote { get; set; }
    public string? Status { get; set; }
    public Guid? SubjectId { get; set; }
    public bool? IsSolved { get; set; }
}