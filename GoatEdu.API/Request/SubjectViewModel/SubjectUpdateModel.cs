namespace GoatEdu.API.Request;

public class SubjectUpdateModel
{
    public string? SubjectName { get; set; } 
    public IFormFile? image { get; set; }
    public string? SubjectCode { get; set; } 
    public string? Information { get; set; } 
    public string? Class { get; set; } 
}