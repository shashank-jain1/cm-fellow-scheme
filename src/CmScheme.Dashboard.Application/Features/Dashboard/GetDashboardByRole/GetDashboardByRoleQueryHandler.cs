using Ardalis.Result;
using CmScheme.Dashboard.Core.Data;
using CmScheme.Dashboard.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetDashboardByRole;

public sealed class GetDashboardByRoleQueryHandler(IDashboardQueryDbContext dbContext)
    : IQueryHandler<GetDashboardByRoleQuery, Result<RoleDashboardDto>>
{
    public async ValueTask<Result<RoleDashboardDto>> Handle(GetDashboardByRoleQuery request, CancellationToken cancellationToken)
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

        RoleDashboardDto dashboard = new RoleDashboardDto
        {
            Role = request.Role,
            Widgets = widgets
        };

        return Result<RoleDashboardDto>.Success(dashboard);
    }
}
