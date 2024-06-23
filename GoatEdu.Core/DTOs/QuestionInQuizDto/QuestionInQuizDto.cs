namespace GoatEdu.Core.DTOs.QuestionInQuizDto;

public class QuestionInQuizDto
{
    public Guid? Id { get; set; }
    public Guid? QuizId { get; set; }
    public string? QuizQuestion { get; set; }
    public string? QuizAnswer1 { get; set; }
    public string? QuizAnswer2 { get; set; }
    public string? QuizAnswer3 { get; set; }
    public string? QuizCorrect { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}