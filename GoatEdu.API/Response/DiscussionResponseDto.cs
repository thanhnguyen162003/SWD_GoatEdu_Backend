using GoatEdu.Core.DTOs;

namespace GoatEdu.API.Response;

public class DiscussionResponseDto
{
    public Guid Id { get; set; }
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionImage { get; set; }
    public int? DiscussionVote { get; set; }
    public bool? IsSolved { get; set; }
    public string? Status { get; set; }
    public DateTime? CreateAt { get; set; }
    public UserAndSubject? UserAndSubject { get; set; }
}