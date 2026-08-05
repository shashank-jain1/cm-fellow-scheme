using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.Registration.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetAdminDashboard;

public sealed class GetAdminDashboardQueryHandler(
    IRegistrationQueryDbContext registrationDbContext,
    IWorkAllocationQueryDbContext workAllocationDbContext,
    IHelpDeskQueryDbContext helpDeskDbContext,
    IAttendanceLeaveQueryDbContext attendanceDbContext,
    IMemoryCache memoryCache)
    : IQueryHandler<GetAdminDashboardQuery, Result<AdminDashboardDto>>
{
    private const string CacheKey = "AdminDashboard";

    public async ValueTask<Result<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
    {
        if (memoryCache.TryGetValue(CacheKey, out AdminDashboardDto? cachedDashboard) && cachedDashboard is not null)
        {
            return Result<AdminDashboardDto>.Success(cachedDashboard);
        }

        int totalRegisteredUsers = await registrationDbContext.UserAccounts.CountAsync(cancellationToken);
        int totalProjects = await workAllocationDbContext.WorkAllocations.Select(w => w.ProjectId).Distinct().CountAsync(cancellationToken);
        int totalSurveysCompleted = await workAllocationDbContext.SurveyRecords.CountAsync(s => s.SurveyStatus == Statuses.Survey.Completed, cancellationToken);
        int totalSurveysPending = await workAllocationDbContext.SurveyRecords.CountAsync(s => s.SurveyStatus != Statuses.Survey.Completed, cancellationToken);
        int totalTicketsOpen = await helpDeskDbContext.Tickets.CountAsync(t => t.Status == Statuses.Ticket.Open, cancellationToken);

        decimal totalCompletionPercentage = await workAllocationDbContext.TaskProgresses
            .Select(t => (decimal?)t.CompletionPercentage)
            .AverageAsync(cancellationToken) ?? 0m;

        int totalPresentDays = await attendanceDbContext.Attendances
            .CountAsync(a => a.AttendanceStatus == Statuses.Attendance.Present, cancellationToken);

        int totalDaysWithRecords = await attendanceDbContext.Attendances
            .Select(a => a.AttendanceDate.Date)
            .Distinct()
            .CountAsync(cancellationToken);

        int totalExpectedAttendance = totalRegisteredUsers * Math.Max(totalDaysWithRecords, 1);
        decimal overallAttendancePercentage = totalExpectedAttendance > 0
            ? Math.Round((decimal)totalPresentDays / totalExpectedAttendance * 100, 1)
            : 0m;

        AdminDashboardDto dashboard = new AdminDashboardDto(
            TotalRegisteredUsers: totalRegisteredUsers,
            TotalProjects: totalProjects,
            TotalSurveysCompleted: totalSurveysCompleted,
            TotalSurveysPending: totalSurveysPending,
            TotalTicketsOpen: totalTicketsOpen,
            OverallAttendancePercentage: overallAttendancePercentage,
            OverallSurveyCompletionPercentage: totalCompletionPercentage);

        MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        memoryCache.Set(CacheKey, dashboard, cacheOptions);

        return Result<AdminDashboardDto>.Success(dashboard);
    }
}
