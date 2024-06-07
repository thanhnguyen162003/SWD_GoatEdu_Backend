using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs;

public class DiscussionUpdateDto
{
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public IFormFile? DiscussionImageConvert { get; set; }
    public List<TagDto.TagDto> Tags { get; set; }
    public bool? IsSolved { get; set; }

}