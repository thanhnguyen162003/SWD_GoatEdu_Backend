using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs;

public class AnswerDto
{
    public Guid? Id { get; set; }
    public string? AnswerName { get; set; }
    public string? AnswerBody { get; set; }
    public string? AnswerBodyHtml { get; set; }
    public Guid? QuestionId { get; set; }
    public int? AnswerVote { get; set; }
    public DateTime? CreatedAt { get; set; }
    public UserInformation? UserInformation { get; set; }
    public bool IsUserVoted { get; set; }
}