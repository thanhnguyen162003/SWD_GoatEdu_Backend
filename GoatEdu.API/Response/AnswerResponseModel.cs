using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Response;

public class AnswerResponseModel
{
    [Required]
    public Guid Id { get; set; }
    public string? AnswerName { get; set; }
    public string? AnswerBody { get; set; }
    public Guid? UserId { get; set; }
    public IFormFile? AnswerImage { get; set; }
    public int? AnswerVote { get; set; }
    public DateTime? CreatedAt { get; set; }
}