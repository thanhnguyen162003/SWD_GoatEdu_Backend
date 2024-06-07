namespace GoatEdu.API.Request;

public class AnswerRequestDto
{
    
    public string? AnswerBody { get; set; }
    public Guid? QuestionId { get; set; }
    public IFormFile? AnswerImage { get; set; }
    public int? AnswerVote { get; set; }
    public DateTime? CreatedAt { get; set; }
}