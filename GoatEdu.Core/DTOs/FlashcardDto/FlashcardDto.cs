namespace GoatEdu.Core.DTOs.FlashcardDto;

public class FlashcardDto
{
    public Guid? id { get; set; }
    public string? flashcardName { get; set; }
    public string? flashcardDescription { get; set; }
    public int? star { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string userId { get; set; }
    public string fullName { get; set; }
    public string userImage { get; set; }
    public string subjectName { get; set; }
    public string subjectId { get; set; }
    public int numberOfFlashcardContent { get; set; }
    public string status { get; set; }
    
}