using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class QuestionInQuizCreateModel
{
    [Required(ErrorMessage = "Quiz question is required!")]
    public string? QuizQuestion { get; set; }
    [Required(ErrorMessage = "Quiz answer 1 is required!")]
    public string? QuizAnswer1 { get; set; }
    [Required(ErrorMessage = "Quiz answer 2 is required!")]
    public string? QuizAnswer2 { get; set; }
    [Required(ErrorMessage = "Quiz answer 3 is required!")]
    public string? QuizAnswer3 { get; set; }
    [Required(ErrorMessage = "Answer correct is required!")]
    public string? QuizCorrect { get; set; }
    
}