using System.Text;
using Ardalis.Result;
using CmScheme.Dashboard.Core.Data;
using CmScheme.Dashboard.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.ExportDashboardToExcel;

public sealed class ExportDashboardToExcelCommandHandler(IDashboardQueryDbContext dbContext)
    : ICommandHandler<ExportDashboardToExcelCommand, Result<byte[]>>
{
    public async ValueTask<Result<byte[]>> Handle(ExportDashboardToExcelCommand request, CancellationToken cancellationToken)
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

        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Widget Name,Type,Role Access,Sort Order");

        foreach (DashboardWidgetDto widget in widgets)
        {
            csv.AppendLine($"\"{widget.WidgetName}\",\"{widget.WidgetType}\",\"{widget.RoleAccess}\",{widget.SortOrder}");
        }

        byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
        return Result<byte[]>.Success(bytes);
    }
}
