namespace GoatEdu.API.Request;

public class NoteRequestDto
{
    public string? NoteName { get; set; }
    public string? NoteBody { get; set; }
    public Guid? UserId { get; set; }
}