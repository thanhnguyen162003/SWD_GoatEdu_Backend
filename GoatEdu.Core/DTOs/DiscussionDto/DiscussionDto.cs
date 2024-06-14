using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs;

public class DiscussionDto
{

    public Guid Id { get; set; }
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionBodyHtml { get; set; }
    public string? DiscussionImage { get; set; }
    public IFormFile? DiscussionImageConvert { get; set; }
    public int? DiscussionVote { get; set; }
    public string? Status { get; set; }
    public ICollection<TagDto.TagDto>? Tags { get; set; }
    public Guid? SubjectId { get; set; }
    public bool? IsSolved { get; set; }
    public DateTime? CreatedAt { get; set; }
    public UserAndSubject? UserAndSubject { get; set; }
    public bool IsUserVoted { get; set; }
}