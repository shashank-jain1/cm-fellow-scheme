using CmScheme.Common.Core.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class ReportService : IReportService
{
    public Task<byte[]> GenerateAttendanceReportAsync(int userId, int month, int year, CancellationToken ct = default)
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
                        .Text("ATTENDANCE REPORT")
                        .FontSize(20).Bold().FontColor(Colors.Blue.Darken2);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text($"User ID: {userId}")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text($"Period: {new DateTime(year, month, 1):MMMM yyyy}")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(20)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Total Working Days:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("22").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Days Present:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("20").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Days Absent:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("2").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Attendance Percentage:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("90.91%").FontSize(11);
                    });

                    column.Item().PaddingTop(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().PaddingTop(10)
                        .Text($"Generated on: {DateTime.UtcNow:dd MMM yyyy HH:mm} UTC")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }

    public Task<byte[]> GeneratePerformanceReportAsync(int userId, CancellationToken ct = default)
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
                        .Text("PERFORMANCE REPORT")
                        .FontSize(20).Bold().FontColor(Colors.Green.Darken2);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text($"User ID: {userId}")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(20)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Overall Rating:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("A").FontSize(11).Bold().FontColor(Colors.Green.Darken2);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Task Completion Rate:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("95%").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Quality Score:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("4.5/5").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Timeliness:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("Excellent").FontSize(11);
                    });

                    column.Item().PaddingTop(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().PaddingTop(10)
                        .Text($"Generated on: {DateTime.UtcNow:dd MMM yyyy HH:mm} UTC")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }

    public Task<byte[]> GenerateLeaveReportAsync(int userId, CancellationToken ct = default)
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
                        .Text("LEAVE REPORT")
                        .FontSize(20).Bold().FontColor(Colors.Orange.Darken2);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text($"User ID: {userId}")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(20)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Total Leave Entitled:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("24 days").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Leave Taken:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("8 days").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Leave Remaining:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("16 days").FontSize(11);
                    });

                    column.Item().PaddingTop(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().PaddingTop(10)
                        .Text($"Generated on: {DateTime.UtcNow:dd MMM yyyy HH:mm} UTC")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }

    public Task<byte[]> GenerateTrainingReportAsync(int trainingId, CancellationToken ct = default)
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
                        .Text("TRAINING REPORT")
                        .FontSize(20).Bold().FontColor(Colors.Purple.Darken2);

                    column.Item().AlignCenter().PaddingBottom(10)
                        .Text($"Training ID: {trainingId}")
                        .FontSize(12).FontColor(Colors.Grey.Medium);

                    column.Item().PaddingBottom(20)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Total Participants:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("30").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Completed:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("28").FontSize(11);
                    });

                    column.Item().PaddingBottom(5).Row(row =>
                    {
                        row.RelativeItem().Text("Completion Rate:").FontSize(11).Bold();
                        row.RelativeItem().AlignRight().Text("93.33%").FontSize(11);
                    });

                    column.Item().PaddingTop(30)
                        .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().PaddingTop(10)
                        .Text($"Generated on: {DateTime.UtcNow:dd MMM yyyy HH:mm} UTC")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }
}
