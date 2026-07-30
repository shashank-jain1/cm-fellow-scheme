using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetFellowDashboard;

public sealed class GetFellowDashboardQueryHandler(
    IRegistrationQueryDbContext registrationDbContext,
    IHelpDeskQueryDbContext helpDeskDbContext,
    IAttendanceLeaveQueryDbContext attendanceDbContext,
    IWorkAllocationQueryDbContext workAllocationDbContext)
    : IQueryHandler<GetFellowDashboardQuery, Result<FellowDashboardDto>>
{
    public async ValueTask<Result<FellowDashboardDto>> Handle(GetFellowDashboardQuery request, CancellationToken cancellationToken)
    {
        UserAccount? userAccount = await registrationDbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.FellowId, cancellationToken);

        if (userAccount is null)
        {
            return Result.NotFound("User account not found.");
        }

        int applicantId = userAccount.ApplicantId;

        int totalTickets = await helpDeskDbContext.Tickets
            .CountAsync(t => t.ApplicantId == applicantId, cancellationToken);

        int openTickets = await helpDeskDbContext.Tickets
            .CountAsync(t => t.ApplicantId == applicantId &&
                (t.Status == Statuses.Ticket.Open || t.Status == Statuses.Ticket.InProgress), cancellationToken);

        int totalPresentDays = await attendanceDbContext.Attendances
            .CountAsync(a => a.ApplicantId == applicantId &&
                a.AttendanceStatus == Statuses.Attendance.Present, cancellationToken);

        int totalDaysWithRecords = await attendanceDbContext.Attendances
            .Where(a => a.ApplicantId == applicantId)
            .Select(a => a.AttendanceDate.Date)
            .Distinct()
            .CountAsync(cancellationToken);

        decimal attendancePercentage = totalDaysWithRecords > 0
            ? Math.Round((decimal)totalPresentDays / totalDaysWithRecords * 100, 1)
            : 0m;

        int leaveBalance = await attendanceDbContext.LeaveBalances
            .Where(lb => lb.ApplicantId == applicantId)
            .SumAsync(lb => lb.AvailableBalance, cancellationToken);

        int pendingTasks = await workAllocationDbContext.TaskProgresses
            .CountAsync(t => t.UserAccountId == request.FellowId &&
                t.WorkStatus != Statuses.WorkStatus.Completed, cancellationToken);

        int completedTasks = await workAllocationDbContext.TaskProgresses
            .CountAsync(t => t.UserAccountId == request.FellowId &&
                t.WorkStatus == Statuses.WorkStatus.Completed, cancellationToken);

        FellowDashboardDto dashboard = new FellowDashboardDto
        {
            TotalTickets = totalTickets,
            OpenTickets = openTickets,
            AttendancePercentage = attendancePercentage,
            LeaveBalanceDays = leaveBalance,
            PendingTasks = pendingTasks,
            CompletedTasks = completedTasks,
            PerformanceScore = 0m
        };

        return Result<FellowDashboardDto>.Success(dashboard);
    }
}
