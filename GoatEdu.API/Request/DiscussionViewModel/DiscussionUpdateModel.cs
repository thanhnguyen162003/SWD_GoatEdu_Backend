namespace GoatEdu.API.Request;

public class DiscussionUpdateModel
{
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public string? DiscussionBodyHtml { get; set; }
    public IFormFile? DiscussionImage { get; set; }
    public List<TagUpdateModel>? Tags { get; set; }
    public bool? IsSolved { get; set; }
}