using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs.ChapterDto;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.API.Request;

public class SubjectCreateModel
{
    [Required(ErrorMessage = "Subject name is required.")]
    public string? SubjectName { get; set; } 
    public IFormFile? image { get; set; }
    [Required(ErrorMessage = "Subject code is required.")]
    public string? SubjectCode { get; set; } 
    [Required(ErrorMessage = "Information is required.")]
    public string? Information { get; set; }
    [Required(ErrorMessage = "Class is required.")]
    public string? Class { get; set; } 
   
}