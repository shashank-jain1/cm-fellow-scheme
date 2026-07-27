using Ardalis.Result;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.Registration.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetAdminDashboard;

public sealed class GetAdminDashboardQueryHandler(
    IRegistrationQueryDbContext registrationDbContext,
    IWorkAllocationQueryDbContext workAllocationDbContext,
    IHelpDeskQueryDbContext helpDeskDbContext)
    : IQueryHandler<GetAdminDashboardQuery, Result<AdminDashboardDto>>
{
    public async ValueTask<Result<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
    {
        int totalRegisteredUsers = await registrationDbContext.Applicants.CountAsync(cancellationToken);
        int totalProjects = await workAllocationDbContext.WorkAllocations.Select(w => w.ProjectId).Distinct().CountAsync(cancellationToken);
        int totalSurveysCompleted = await workAllocationDbContext.SurveyRecords.CountAsync(s => s.SurveyStatus == "Completed", cancellationToken);
        int totalSurveysPending = await workAllocationDbContext.SurveyRecords.CountAsync(s => s.SurveyStatus != "Completed", cancellationToken);
        int totalTicketsOpen = await helpDeskDbContext.Tickets.CountAsync(t => t.Status == "Open", cancellationToken);

        decimal totalCompletionPercentage = await workAllocationDbContext.TaskProgresses
            .Select(t => (decimal?)t.CompletionPercentage)
            .AverageAsync(cancellationToken) ?? 0m;

        AdminDashboardDto dashboard = new AdminDashboardDto(
            TotalRegisteredUsers: totalRegisteredUsers,
            TotalProjects: totalProjects,
            TotalSurveysCompleted: totalSurveysCompleted,
            TotalSurveysPending: totalSurveysPending,
            TotalTicketsOpen: totalTicketsOpen,
            OverallAttendancePercentage: 0m,
            OverallSurveyCompletionPercentage: totalCompletionPercentage);

        return Result<AdminDashboardDto>.Success(dashboard);
    }
}
