namespace GoatEdu.API.Request;

public class ChapterCreateDto
{
    public Guid Id { get; set; } 
    public string ChapterName { get; set; } 
    public Guid SubjectId { get; set; } 
    public int? ChapterLevel { get; set; } 
    
}