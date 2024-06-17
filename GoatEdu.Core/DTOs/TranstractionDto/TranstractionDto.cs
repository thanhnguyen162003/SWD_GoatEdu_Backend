namespace GoatEdu.Core.DTOs.TranstractionDto;

public class TranstractionDto
{
    public string transtractionName { get; set; }
    public string note { get; set; }
    public DateTime createdAt { get; set; }
    public Guid SubcriptionId { get; set; }
}