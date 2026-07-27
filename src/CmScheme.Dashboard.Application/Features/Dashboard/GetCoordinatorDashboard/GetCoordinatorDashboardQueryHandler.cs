using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.Registration.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetCoordinatorDashboard;

public sealed class GetCoordinatorDashboardQueryHandler(
    IRegistrationQueryDbContext registrationDbContext,
    IWorkAllocationQueryDbContext workAllocationDbContext,
    IAttendanceLeaveQueryDbContext attendanceDbContext)
    : IQueryHandler<GetCoordinatorDashboardQuery, Result<CoordinatorDashboardDto>>
{
    public async ValueTask<Result<CoordinatorDashboardDto>> Handle(GetCoordinatorDashboardQuery request, CancellationToken cancellationToken)
    {
        int teamSize = await registrationDbContext.Applicants.CountAsync(cancellationToken);
        int activeProjects = await workAllocationDbContext.WorkAllocations
            .CountAsync(w => w.ActiveStatus, cancellationToken);
        int pendingTasks = await workAllocationDbContext.TaskProgresses
            .CountAsync(t => t.WorkStatus != Statuses.WorkStatus.Completed, cancellationToken);
        int completedSurveys = await workAllocationDbContext.SurveyRecords
            .CountAsync(s => s.SurveyStatus == Statuses.Survey.Completed, cancellationToken);
        int pendingSurveys = await workAllocationDbContext.SurveyRecords
            .CountAsync(s => s.SurveyStatus != Statuses.Survey.Completed, cancellationToken);

        int totalPresentDays = await attendanceDbContext.Attendances
            .CountAsync(a => a.AttendanceStatus == Statuses.Attendance.Present, cancellationToken);

        int totalDaysWithRecords = await attendanceDbContext.Attendances
            .Select(a => a.AttendanceDate.Date)
            .Distinct()
            .CountAsync(cancellationToken);

        int totalExpectedAttendance = teamSize * Math.Max(totalDaysWithRecords, 1);
        decimal teamAttendancePercentage = totalExpectedAttendance > 0
            ? Math.Round((decimal)totalPresentDays / totalExpectedAttendance * 100, 1)
            : 0m;

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
