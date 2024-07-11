using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class ChapterCreateModel
{
    [Required(ErrorMessage = "Chapter name is required.")]
    public string ChapterName { get; set; }
    [Required(ErrorMessage = "Subject id is required.")]
    public Guid SubjectId { get; set; }
    [Required(ErrorMessage = "Chapter level is required.")]
    public int ChapterLevel { get; set; } 
}