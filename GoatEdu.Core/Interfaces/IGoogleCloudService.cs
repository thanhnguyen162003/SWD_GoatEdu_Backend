using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.Interfaces;

public interface IGoogleCloudService
{
    Task<string?> UploadFileAsync(IFormFile file,string objectName);
    Task DeleteFileAsync(string fileNameForStorage);
}