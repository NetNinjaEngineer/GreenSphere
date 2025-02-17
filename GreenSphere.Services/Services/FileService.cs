using GreenSphere.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Services.Services;

public sealed class FileService : IFileService
{
    public async Task<string> UploadFileAsync(IFormFile? file, string locationFolder)
    {
        if (file == null || file.Length == 0)
            return string.Empty;

        var uniqueFileName = $"{DateTimeOffset.Now:yyyyMMdd_HHmmssfff}_{file.FileName}";

        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", locationFolder);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, uniqueFileName);

        await using var fileStream = new FileStream(filePath, FileMode.CreateNew);
        await file.CopyToAsync(fileStream);

        return uniqueFileName;
    }

    public bool DeleteFileFromPath(string filePath, string locationFolder)
    {
        if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(locationFolder))
            return false;

        var resourcePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "Uploads",
            locationFolder,
            filePath);

        if (!File.Exists(resourcePath)) return false;

        File.Delete(resourcePath);

        return true;

    }
}