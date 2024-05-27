using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.Interfaces.CloudinaryInterfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadAsync(IFormFile file);
}