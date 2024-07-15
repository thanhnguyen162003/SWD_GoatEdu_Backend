namespace GoatEdu.API.Response.TheoryViewModel;

public class TheoryResponseModel
{
    public Guid? Id { get; set; }
    public string? TheoryName { get; set; }
    public string? Image { get; set; }
    public string? TheoryContent { get; set; }
    public string? TheoryContentHtml { get; set; }
    public Guid? LessonId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int FlashcardCount { get; set; }
}