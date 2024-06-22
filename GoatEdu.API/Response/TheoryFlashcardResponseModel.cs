namespace GoatEdu.API.Response;

public class TheoryFlashcardResponseModel
{
    public Guid? Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public Guid? TheoryId { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}