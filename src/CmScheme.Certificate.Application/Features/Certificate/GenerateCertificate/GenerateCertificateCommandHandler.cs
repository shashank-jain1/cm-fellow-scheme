using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using CmScheme.Common.Core;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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

        if (!string.Equals(certificate.Status, Statuses.Certificate.Approved, StringComparison.OrdinalIgnoreCase))
        {
            return Result.Invalid(new ValidationError("Certificate must be approved before generation."));
        }

        string uploadDir = Path.Combine("wwwroot", "uploads", "certificates");
        Directory.CreateDirectory(uploadDir);

        string uniqueFileName = $"{certificate.CertificateId}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
        string filePath = Path.Combine(uploadDir, uniqueFileName);

        byte[] pdfBytes = GeneratePdf(certificate);
        await File.WriteAllBytesAsync(filePath, pdfBytes, cancellationToken);

        certificate.CertificatePdfPath = $"/uploads/certificates/{uniqueFileName}";
        certificate.CertificateIssueDate = DateTime.UtcNow;
        certificate.Status = Statuses.Certificate.Issued;
        certificate.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(certificate.CertificatePdfPath);
    }

    private static byte[] GeneratePdf(CertificateApplication certificate)
    {
        IDocument document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.MarginHorizontal(60);
                page.MarginVertical(40);

                page.Content().Column(column =>
                {
                    column.Item().PaddingBottom(10).Row(row =>
                    {
                        row.RelativeItem().AlignLeft().Text("ATAL BIHARI VAJPAYEE INSTITUTE OF")
                            .FontSize(11).Bold().FontColor(Colors.Grey.Medium);
                        row.RelativeItem().AlignRight().Text($"Certificate No: CERT-{certificate.CertificateId:D4}")
                            .FontSize(10).FontColor(Colors.Grey.Medium);
                    });

                    column.Item().PaddingBottom(4).Text("GOOD GOVERNANCE AND POLICY ANALYSIS")
                        .FontSize(11).Bold().FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text("CERTIFICATE OF COMPLETION")
                        .FontSize(26).Bold().FontColor(Colors.Green.Darken3);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text("This is to certify that")
                        .FontSize(13).FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text(certificate.ApplicantName)
                        .FontSize(22).Bold().FontColor(Colors.Grey.Darken4);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text("has successfully completed the program")
                        .FontSize(13).FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text(certificate.ProgramName)
                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken2);

                    column.Item().AlignCenter().PaddingBottom(8)
                        .Text($"Duration: {certificate.StartDate:dd MMM yyyy} to {certificate.EndDate:dd MMM yyyy} ({certificate.DurationDays} days)")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Verified By:").FontSize(10).Bold().FontColor(Colors.Grey.Medium);
                            col.Item().Text(certificate.VerifiedBy ?? "N/A").FontSize(11);
                        });

                        row.RelativeItem().AlignCenter().Column(col =>
                        {
                            col.Item().Text("Date of Issue:").FontSize(10).Bold().FontColor(Colors.Grey.Medium);
                            col.Item().Text(certificate.CertificateIssueDate?.ToString("dd MMM yyyy") ?? DateTime.UtcNow.ToString("dd MMM yyyy")).FontSize(11);
                        });

                        row.RelativeItem().AlignRight().Column(col =>
                        {
                            col.Item().Text("Certificate ID:").FontSize(10).Bold().FontColor(Colors.Grey.Medium);
                            col.Item().Text($"CERT-{certificate.CertificateId:D4}").FontSize(11);
                        });
                    });

                    column.Item().PaddingTop(20)
                        .AlignCenter()
                        .Text("Atal Bihari Vajpayee Institute of Good Governance and Policy Analysis")
                        .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                });
            });
        });

        return document.GeneratePdf();
    }
}
