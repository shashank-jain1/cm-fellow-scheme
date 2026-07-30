using Ardalis.Result;
using CmScheme.Dashboard.Core.Data;
using CmScheme.Dashboard.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToPdf;

public sealed class ExportDashboardToPdfCommandHandler(IDashboardQueryDbContext dbContext)
    : ICommandHandler<ExportDashboardToPdfCommand, Result<byte[]>>
{
    public async ValueTask<Result<byte[]>> Handle(ExportDashboardToPdfCommand request, CancellationToken cancellationToken)
    {
        List<DashboardWidgetDto> widgets = await dbContext.DashboardWidgets
            .Where(w => w.IsActive && (w.RoleAccess.Contains(request.Role) || w.RoleAccess == "*"))
            .OrderBy(w => w.SortOrder)
            .Select(w => new DashboardWidgetDto
            {
                WidgetId = w.WidgetId,
                WidgetName = w.WidgetName,
                WidgetType = w.WidgetType,
                RoleAccess = w.RoleAccess,
                SortOrder = w.SortOrder
            })
            .ToListAsync(cancellationToken);

        QuestPDF.Settings.License = LicenseType.Community;

        byte[] pdfBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.Header().Text($"Dashboard Report - {request.Role}")
                    .FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                page.Content().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Widget Name").Bold();
                        header.Cell().Text("Type").Bold();
                        header.Cell().Text("Role Access").Bold();
                        header.Cell().Text("Sort Order").Bold();
                    });

                    foreach (DashboardWidgetDto widget in widgets)
                    {
                        table.Cell().Text(widget.WidgetName);
                        table.Cell().Text(widget.WidgetType);
                        table.Cell().Text(widget.RoleAccess);
                        table.Cell().Text(widget.SortOrder.ToString());
                    }
                });
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Generated on ");
                    text.CurrentPageNumber();
                });
            });
        }).GeneratePdf();

        return Result<byte[]>.Success(pdfBytes);
    }
}
