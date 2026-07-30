namespace CmScheme.Common.Core.Services;

public interface IFileUploadService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string folder, CancellationToken ct = default);
    Task<bool> DeleteAsync(string fileUrl, CancellationToken ct = default);
    string GetFileUrl(string relativePath);
}
