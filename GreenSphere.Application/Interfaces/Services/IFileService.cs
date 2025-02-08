using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Interfaces.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string locationFolder);
}