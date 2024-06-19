namespace GoatEdu.API.Request.LessonViewModel;

public class LessonUpdateModel
{
    public string? LessonName { get; set; }
    public string? LessonBody { get; set; }
    public string? LessonMaterial { get; set; }
    public int? DisplayOrder { get; set; }
}