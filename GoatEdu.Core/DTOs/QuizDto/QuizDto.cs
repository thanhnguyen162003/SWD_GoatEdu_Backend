namespace GoatEdu.Core.DTOs.QuizDto;

public class QuizDto
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
    public ICollection<QuestionInQuizDto.QuestionInQuizDto> QuestionInQuizzes { get; set; }
    public int QuestionCount { get; set; }
}