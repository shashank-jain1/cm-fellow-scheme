using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.Performance.Core.Data;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
using CmScheme.Training.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetFellowDashboard;

public sealed class GetFellowDashboardQueryHandler(
    IRegistrationQueryDbContext registrationDbContext,
    IHelpDeskQueryDbContext helpDeskDbContext,
    IAttendanceLeaveQueryDbContext attendanceDbContext,
    IWorkAllocationQueryDbContext workAllocationDbContext,
    ITrainingQueryDbContext trainingDbContext,
    IPerformanceQueryDbContext performanceDbContext)
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

        var surveyTotals = await workAllocationDbContext.TaskProgresses
            .Where(t => t.UserAccountId == request.FellowId)
            .GroupBy(t => 1)
            .Select(g => new
            {
                Assigned = g.Sum(t => t.NumberOfSurveys),
                Completed = g.Sum(t => t.CompletedSurveys),
                Allocations = g.Select(t => t.WorkAllocationId).Distinct().Count(),
            })
            .FirstOrDefaultAsync(cancellationToken);

        int completedSurveys = surveyTotals?.Completed ?? 0;
        int pendingSurveys = Math.Max(0, (surveyTotals?.Assigned ?? 0) - completedSurveys);

        DateTime today = DateTime.UtcNow.Date;
        int upcomingTraining = await trainingDbContext.TrainingSchedules
            .CountAsync(ts => ts.Date >= today
                              && ts.Status != Statuses.Training.Cancelled
                              && ts.Status != Statuses.Training.Closed,
                cancellationToken);

        decimal performanceScore = await performanceDbContext.PerformanceEvaluations
            .Where(pe => pe.ApplicantNumber == applicantId.ToString())
            .OrderByDescending(pe => pe.EvaluationDate)
            .Select(pe => pe.PerformanceScore)
            .FirstOrDefaultAsync(cancellationToken);

        FellowDashboardDto dashboard = new FellowDashboardDto
        {
            TotalTickets = totalTickets,
            OpenTickets = openTickets,
            AttendancePercentage = attendancePercentage,
            LeaveBalanceDays = leaveBalance,
            PendingTasks = pendingTasks,
            CompletedTasks = completedTasks,
            PerformanceScore = performanceScore,
            TotalAssignedProjects = surveyTotals?.Allocations ?? 0,
            CompletedSurveys = completedSurveys,
            PendingSurveys = pendingSurveys,
            UpcomingTraining = upcomingTraining,
            RecentActivity = await BuildRecentActivityAsync(applicantId, cancellationToken),
        };

        return Result<FellowDashboardDto>.Success(dashboard);
    }

    /// <summary>
    /// Short human-readable summary of the fellow's most recent recorded action, used by
    /// the "Recent Activity" panel on the fellow dashboard.
    /// </summary>
    private async ValueTask<string> BuildRecentActivityAsync(int applicantId, CancellationToken cancellationToken)
    {
        var lastAttendance = await attendanceDbContext.Attendances
            .Where(a => a.ApplicantId == applicantId)
            .OrderByDescending(a => a.AttendanceDate)
            .Select(a => new { a.AttendanceDate, a.AttendanceStatus })
            .FirstOrDefaultAsync(cancellationToken);

        if (lastAttendance is not null)
        {
            return $"Attendance marked {lastAttendance.AttendanceStatus} on {lastAttendance.AttendanceDate:dd MMM yyyy}";
        }

        var lastTicket = await helpDeskDbContext.Tickets
            .Where(t => t.ApplicantId == applicantId)
            .OrderByDescending(t => t.CreatedOn)
            .Select(t => new { t.CreatedOn, t.Status })
            .FirstOrDefaultAsync(cancellationToken);

        if (lastTicket is not null)
        {
            return $"Support ticket ({lastTicket.Status}) raised on {lastTicket.CreatedOn:dd MMM yyyy}";
        }

        return "No recent activity";
    }
}
