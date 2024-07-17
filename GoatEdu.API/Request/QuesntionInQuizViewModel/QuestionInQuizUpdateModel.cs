using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class QuestionInQuizUpdateModel
{
    [Required(ErrorMessage = "Id is required!")]
    public string? Id { get; set; }
    public string? QuizQuestion { get; set; }
    public string? QuizAnswer1 { get; set; }
    public string? QuizAnswer2 { get; set; }
    public string? QuizAnswer3 { get; set; }
    public string? QuizCorrect { get; set; }
}