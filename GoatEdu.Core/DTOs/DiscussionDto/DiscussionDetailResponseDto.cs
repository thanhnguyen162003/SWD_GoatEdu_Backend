namespace GoatEdu.Core.DTOs;

public class DiscussionDetailResponseDto
{
    public Guid Id { get; set; }
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionImage { get; set; }
    public int? DiscussionVote { get; set; }
    public bool? IsSolved { get; set; }
    public string? Status { get; set; }
    public UserAndSubject? UserAndSubject { get; set; }
}