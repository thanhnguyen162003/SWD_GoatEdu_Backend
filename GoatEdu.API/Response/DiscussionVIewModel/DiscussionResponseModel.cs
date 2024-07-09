using GoatEdu.Core.DTOs;

namespace GoatEdu.API.Response;

public class DiscussionResponseModel
{
    public Guid Id { get; set; }
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionBodyHtml { get; set; }
    public int? DiscussionVote { get; set; }
    public bool? IsSolved { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public ICollection<TagResponseModel>? Tags { get; set; }
    public UserAndSubject? UserAndSubject { get; set; }
    public int CommentCount { get; set; }
    public bool IsUserVoted { get; set; }
}