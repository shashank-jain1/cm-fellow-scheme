using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;

namespace CmScheme.Certificate.Application.Features.Certificate.GenerateCertificate;

public sealed class GenerateCertificateCommandHandler(ICertificateCommandDbContext dbContext)
    : ICommandHandler<GenerateCertificateCommand, Result<string>>
{
    public async ValueTask<Result<string>> Handle(
        GenerateCertificateCommand request,
        CancellationToken cancellationToken)
    {
        CertificateApplication? certificate = await dbContext.CertificateApplications
            .FirstOrDefaultAsync(c => c.CertificateId == request.CertificateId, cancellationToken);

        if (certificate is null)
        {
            return Result.NotFound("Certificate not found.");
        }

        if (certificate.Status != "Approved")
        {
            return Result.Invalid(new ValidationError("Certificate must be approved before generation."));
        }

        string uploadDir = Path.Combine("wwwroot", "uploads", "certificates");
        Directory.CreateDirectory(uploadDir);

        string uniqueFileName = $"{certificate.CertificateId}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
        string filePath = Path.Combine(uploadDir, uniqueFileName);

        string certificateContent = GenerateCertificatePdfContent(certificate);
        await File.WriteAllTextAsync(filePath, certificateContent, cancellationToken);

        certificate.CertificatePdfPath = $"/uploads/certificates/{uniqueFileName}";
        certificate.CertificateIssueDate = DateTime.UtcNow;
        certificate.Status = "Issued";
        certificate.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(certificate.CertificatePdfPath);
    }

    private static string GenerateCertificatePdfContent(CertificateApplication certificate)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Certificate of Completion</title>
    <style>
        body {{ font-family: Arial, sans-serif; text-align: center; padding: 50px; }}
        .certificate {{ border: 3px solid #333; padding: 40px; margin: 20px auto; max-width: 800px; }}
        .title {{ font-size: 28px; font-weight: bold; color: #2c5f2d; margin-bottom: 20px; }}
        .name {{ font-size: 24px; font-weight: bold; color: #333; margin: 20px 0; }}
        .details {{ font-size: 16px; color: #555; margin: 10px 0; }}
        .signature {{ margin-top: 50px; font-size: 14px; color: #777; }}
    </style>
</head>
<body>
    <div class='certificate'>
        <div class='title'>CERTIFICATE OF COMPLETION</div>
        <p class='details'>This is to certify that</p>
        <div class='name'>{certificate.ApplicantName}</div>
        <p class='details'>has successfully completed the program</p>
        <p class='details'><strong>{certificate.ProgramName}</strong></p>
        <p class='details'>Duration: {certificate.StartDate:dd MMM yyyy} to {certificate.EndDate:dd MMM yyyy} ({certificate.DurationDays} days)</p>
        <p class='details'>Certificate ID: {certificate.CertificateId}</p>
        <p class='details'>Issue Date: {certificate.CertificateIssueDate:dd MMM yyyy}</p>
        <div class='signature'>
            <p>Verified By: {certificate.VerifiedBy ?? "N/A"}</p>
            <p>Atal Bihari Vajpayee Institute of Good Governance and Policy Analysis</p>
        </div>
    </div>
</body>
</html>";
    }
}
