namespace GoatEdu.API.Response.QuizViewModel;

public class QuizDetailResponseModel
{
    public Guid Id { get; set; }
    public string? Quiz1 { get; set; }
    public int? QuizLevel { get; set; }
    public Guid? LessonId { get; set; }
    public Guid? ChapterId { get; set; }
    public Guid? SubjectId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsRequire { get; set; }
    public ICollection<QuestionInQuizResponseModel> QuestionInQuizzes { get; set; }
    public int QuestionCount { get; set; } 
}