using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs;

namespace GoatEdu.API.Response;

public class AnswerResponseModel
{
    public Guid Id { get; set; }
    public string? AnswerName { get; set; }
    public string? AnswerBody { get; set; }
    public IFormFile? AnswerImage { get; set; }
    public int? AnswerVote { get; set; }
    public DateTime? CreatedAt { get; set; }
    public UserInformation? UserInformation { get; set; }
    public bool IsUserVoted { get; set; }
}