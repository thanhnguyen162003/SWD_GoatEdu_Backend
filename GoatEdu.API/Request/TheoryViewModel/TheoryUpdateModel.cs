namespace GoatEdu.API.Request.TheoryViewModel;

public class TheoryUpdateModel
{
    public string? TheoryName { get; set; }
    public string? TheoryContent { get; set; }
    public IFormFile? FormFile { get; set; }
    public IFormFile? ImageFile { get; set; } 
}