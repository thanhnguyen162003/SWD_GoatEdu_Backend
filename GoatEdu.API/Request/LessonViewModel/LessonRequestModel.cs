using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request.LessonViewModel;

public class LessonRequestModel
{
    [Required(ErrorMessage = "Lesson name is required!")]
    public string? LessonName { get; set; }
    [Required(ErrorMessage = "Lesson body is required!")]
    public string? LessonBody { get; set; }
    [Required(ErrorMessage = "Lesson material is required!")]
    public string? LessonMaterial { get; set; }
    [Required(ErrorMessage = "Display order is required!")]
    public int? DisplayOrder { get; set; }
}