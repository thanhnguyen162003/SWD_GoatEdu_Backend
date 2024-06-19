namespace GoatEdu.Core.DTOs.FlashcardDto;

public class LessonDto
{
    public Guid Id { get; set; }
    public string? LessonName { get; set; }
    public string? LessonBody { get; set; }
    public string? LessonMaterial { get; set; }
    public Guid? ChapterId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? DisplayOrder { get; set; }
    public int? TheoryCount { get; set; }
    public int? QuizCount { get; set; }
}