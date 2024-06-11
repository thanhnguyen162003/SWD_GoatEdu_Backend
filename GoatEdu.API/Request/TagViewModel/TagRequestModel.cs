using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request;

public class TagRequestModel
{
    [Required(ErrorMessage = "Tag name is required.")]
    public string? TagName { get; set; }
}