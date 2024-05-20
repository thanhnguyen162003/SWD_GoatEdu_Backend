namespace GoatEdu.Core.DTOs.NoteDto;

public class NoteResponseDto
{
    public Guid? Id { get; set; }
    public string? NoteName { get; set; }
    public string? NoteBody { get; set; }
    public Guid? UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}