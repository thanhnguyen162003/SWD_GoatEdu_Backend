using GoatEdu.Core.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GoatEdu.Core.Services;

public class GoogleCloudService : IGoogleCloudService
{
    // File name should be like the line below
    // projects/_/buckets/BUCKET_NAME/objects/OBJECT_NAME   
    
    private readonly GoogleCredential googleCredential;
    private readonly StorageClient storageClient;
    private readonly string bucketName;

    public GoogleCloudService(IConfiguration configuration)
    {
        googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
        storageClient = StorageClient.Create(googleCredential);
        bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
    }

    public async Task<string?> UploadFileAsync(IFormFile file,string objectName)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var typeName = Path.GetExtension(file.FileName);
        var fileNameForStorage = GenerateFileName(objectName, typeName);
        var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, file.ContentType, memoryStream);
        if (dataObject is null)
        {
            return null;
        }

        var publicUrl = $"https://storage.googleapis.com/{bucketName}/{fileNameForStorage}";
        return publicUrl;
    }

    public async Task DeleteFileAsync(string fileNameForStorage)
    {
        await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
    }

    private string GenerateFileName(string objectName, string typeName)
    {
        var id = Guid.NewGuid().ToString();
        var num = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        var fileName = $"{objectName}/{typeName}/{id + num}";
        return fileName;
    }
}
