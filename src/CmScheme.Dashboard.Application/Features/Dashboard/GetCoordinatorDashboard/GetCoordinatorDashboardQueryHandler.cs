using Ardalis.Result;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.Registration.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetCoordinatorDashboard;

public sealed class GetCoordinatorDashboardQueryHandler(
    IRegistrationQueryDbContext registrationDbContext,
    IWorkAllocationQueryDbContext workAllocationDbContext)
    : IQueryHandler<GetCoordinatorDashboardQuery, Result<CoordinatorDashboardDto>>
{
    public async ValueTask<Result<CoordinatorDashboardDto>> Handle(GetCoordinatorDashboardQuery request, CancellationToken cancellationToken)
    {
        int teamSize = await registrationDbContext.Applicants.CountAsync(cancellationToken);
        int activeProjects = await workAllocationDbContext.WorkAllocations
            .CountAsync(w => w.ActiveStatus, cancellationToken);
        int pendingTasks = await workAllocationDbContext.TaskProgresses
            .CountAsync(t => t.WorkStatus != "Completed", cancellationToken);
        int completedSurveys = await workAllocationDbContext.SurveyRecords
            .CountAsync(s => s.SurveyStatus == "Completed", cancellationToken);
        int pendingSurveys = await workAllocationDbContext.SurveyRecords
            .CountAsync(s => s.SurveyStatus != "Completed", cancellationToken);

        decimal teamAttendancePercentage = 0m;

        CoordinatorDashboardDto dashboard = new CoordinatorDashboardDto(
            TeamSize: teamSize,
            ActiveProjects: activeProjects,
            PendingTasks: pendingTasks,
            CompletedSurveys: completedSurveys,
            PendingSurveys: pendingSurveys,
            TeamAttendancePercentage: teamAttendancePercentage);

        return Result<CoordinatorDashboardDto>.Success(dashboard);
    }
}
