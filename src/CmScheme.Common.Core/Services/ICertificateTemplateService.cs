namespace CmScheme.Common.Core.Services;

public interface ICertificateTemplateService
{
    Task<byte[]> GenerateCompletionCertificateAsync(string fellowName, string programName, DateTime completionDate, string certificateNumber, CancellationToken ct = default);
    Task<byte[]> GenerateExperienceLetterAsync(string fellowName, string designation, DateTime startDate, DateTime endDate, string supervisorName, CancellationToken ct = default);
    Task<byte[]> AddQrCodeAsync(byte[] pdfContent, string verificationUrl, CancellationToken ct = default);
}
