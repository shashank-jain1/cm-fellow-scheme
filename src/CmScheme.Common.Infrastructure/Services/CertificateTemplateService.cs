using CmScheme.Common.Core.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class CertificateTemplateService : ICertificateTemplateService
{
    public Task<byte[]> GenerateCompletionCertificateAsync(
        string fellowName,
        string programName,
        DateTime completionDate,
        string certificateNumber,
        CancellationToken ct = default)
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
                        row.RelativeItem().AlignLeft()
                            .Text("ATAL BIHARI VAJPAYEE INSTITUTE OF")
                            .FontSize(11).Bold().FontColor(Colors.Grey.Medium);
                        row.RelativeItem().AlignRight()
                            .Text($"Certificate No: {certificateNumber}")
                            .FontSize(10).FontColor(Colors.Grey.Medium);
                    });

                    column.Item().PaddingBottom(4)
                        .Text("GOOD GOVERNANCE AND POLICY ANALYSIS")
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
                        .Text(fellowName)
                        .FontSize(22).Bold().FontColor(Colors.Grey.Darken4);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text("has successfully completed the program")
                        .FontSize(13).FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text(programName)
                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken2);

                    column.Item().AlignCenter().PaddingBottom(8)
                        .Text($"Date of Completion: {completionDate:dd MMM yyyy}")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().AlignCenter().Column(col =>
                        {
                            col.Item().Text("Date of Issue:").FontSize(10).Bold().FontColor(Colors.Grey.Medium);
                            col.Item().Text(completionDate.ToString("dd MMM yyyy")).FontSize(11);
                        });

                        row.RelativeItem().AlignRight().Column(col =>
                        {
                            col.Item().Text("Certificate ID:").FontSize(10).Bold().FontColor(Colors.Grey.Medium);
                            col.Item().Text(certificateNumber).FontSize(11);
                        });
                    });

                    column.Item().PaddingTop(20)
                        .AlignCenter()
                        .Text("Atal Bihari Vajpayee Institute of Good Governance and Policy Analysis")
                        .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }

    public Task<byte[]> GenerateExperienceLetterAsync(
        string fellowName,
        string designation,
        DateTime startDate,
        DateTime endDate,
        string supervisorName,
        CancellationToken ct = default)
    {
        IDocument document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginHorizontal(50);
                page.MarginVertical(40);

                page.Content().Column(column =>
                {
                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text("ATAL BIHARI VAJPAYEE INSTITUTE OF")
                        .FontSize(12).Bold().FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text("GOOD GOVERNANCE AND POLICY ANALYSIS")
                        .FontSize(12).Bold().FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(5)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().AlignCenter().PaddingBottom(30)
                        .Text("EXPERIENCE LETTER")
                        .FontSize(22).Bold().FontColor(Colors.Blue.Darken2);

                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text($"Date: {DateTime.UtcNow:dd MMM yyyy}")
                        .FontSize(11).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(15)
                        .Text("To Whom It May Concern,").FontSize(12);

                    column.Item().PaddingBottom(15)
                        .Text($"This is to certify that {fellowName} has been associated with the Atal Bihari Vajpayee Institute of Good Governance and Policy Analysis as {designation} from {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}.")
                        .FontSize(12).LineHeight(1.5f);

                    column.Item().PaddingBottom(15)
                        .Text($"During this period, {fellowName} has demonstrated professional competence, dedication, and a strong work ethic. Their contributions have been valuable to the organization.")
                        .FontSize(12).LineHeight(1.5f);

                    column.Item().PaddingBottom(15)
                        .Text("We wish them all the best in their future endeavors.")
                        .FontSize(12).LineHeight(1.5f);

                    column.Item().PaddingBottom(15)
                        .Text($"Supervised by: {supervisorName}")
                        .FontSize(12);

                    column.Item().PaddingTop(40)
                        .LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);

                    column.Item().PaddingTop(10)
                        .Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("Authorized Signatory").FontSize(10).Bold();
                                col.Item().Text("Atal Bihari Vajpayee Institute of").FontSize(10).FontColor(Colors.Grey.Medium);
                                col.Item().Text("Good Governance and Policy Analysis").FontSize(10).FontColor(Colors.Grey.Medium);
                            });
                            row.RelativeItem().AlignRight().Column(col =>
                            {
                                col.Item().Text($"Period: {startDate:dd MMM yyyy} - {endDate:dd MMM yyyy}").FontSize(10).FontColor(Colors.Grey.Medium);
                            });
                        });
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }

    public Task<byte[]> AddQrCodeAsync(byte[] pdfContent, string verificationUrl, CancellationToken ct = default)
    {
        IDocument document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginHorizontal(40);
                page.MarginVertical(30);

                page.Content().Column(column =>
                {
                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text("CERTIFICATE VERIFICATION")
                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken2);

                    column.Item().PaddingBottom(20)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().AlignCenter().PaddingBottom(20)
                        .Text("Scan the QR code below to verify this certificate")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Border(1).BorderColor(Colors.Grey.Lighten1).Padding(20)
                        .Text(verificationUrl)
                        .FontSize(10).FontColor(Colors.Blue.Medium).Underline();

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text("Use a QR code scanner to verify authenticity")
                        .FontSize(10).FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text($"Verification URL: {verificationUrl}")
                        .FontSize(10).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingTop(20)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().PaddingTop(10)
                        .AlignCenter()
                        .Text("Atal Bihari Vajpayee Institute of Good Governance and Policy Analysis")
                        .FontSize(9).Italic().FontColor(Colors.Grey.Medium);
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }
}
