namespace GoatEdu.API.Response.LessonViewModel;

public class LessonResponseModel
{
    public Guid Id { get; set; }
    public string? LessonName { get; set; }
    public Guid? ChapterId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? DisplayOrder { get; set; }
}