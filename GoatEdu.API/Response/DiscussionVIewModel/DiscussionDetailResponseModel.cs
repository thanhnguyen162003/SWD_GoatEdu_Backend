using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;

namespace GoatEdu.API.Response;

public class DiscussionDetailResponseModel
{
    public Guid Id { get; set; }
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionBodyHtml { get; set; }
    public string? DiscussionImage { get; set; }
    public int? DiscussionVote { get; set; }
    public bool? IsSolved { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public ICollection<TagResponseModel>? Tags { get; set; }
    public UserAndSubject? UserAndSubject { get; set; }
    public int CommentCount { get; set; }
    public bool IsUserVoted { get; set; }
}