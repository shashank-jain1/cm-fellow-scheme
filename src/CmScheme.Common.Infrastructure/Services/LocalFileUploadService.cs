using CmScheme.Common.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class FileStorageOptions
{
    public string BasePath { get; set; } = null!;
    public string BaseUrl { get; set; } = null!;
}

public sealed class LocalFileUploadService(
    IOptions<FileStorageOptions> options,
    IWebHostEnvironment environment,
    ILogger<LocalFileUploadService> logger) : IFileUploadService
{
    private readonly FileStorageOptions _options = options.Value;

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string folder, CancellationToken ct = default)
    {
        string extension = Path.GetExtension(fileName);
        string uniqueFileName = $"{Guid.NewGuid()}{extension}";

        string uploadPath = Path.Combine(environment.WebRootPath, _options.BasePath, folder);
        Directory.CreateDirectory(uploadPath);

        string filePath = Path.Combine(uploadPath, uniqueFileName);

        await using FileStream outputStream = new(filePath, FileMode.Create);
        await fileStream.CopyToAsync(outputStream, ct);

        string relativePath = $"{_options.BasePath}/{folder}/{uniqueFileName}";
        logger.LogInformation("File uploaded: {RelativePath}", relativePath);

        return relativePath;
    }

    public Task<bool> DeleteAsync(string fileUrl, CancellationToken ct = default)
    {
        string relativePath = fileUrl.TrimStart('/');
        string fullPath = Path.Combine(environment.WebRootPath, relativePath);

        if (!File.Exists(fullPath))
        {
            logger.LogWarning("File not found for deletion: {FileUrl}", fileUrl);
            return Task.FromResult(false);
        }

        File.Delete(fullPath);
        logger.LogInformation("File deleted: {FileUrl}", fileUrl);
        return Task.FromResult(true);
    }

    public string GetFileUrl(string relativePath)
    {
        return $"/{relativePath.TrimStart('/')}";
    }
}
