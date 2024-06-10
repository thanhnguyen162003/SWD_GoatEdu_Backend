using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class NoteRequestModel
{
    [Required(ErrorMessage = "Note name is required!")]
    public string? NoteName { get; set; }
    public string? NoteBody { get; set; }
}