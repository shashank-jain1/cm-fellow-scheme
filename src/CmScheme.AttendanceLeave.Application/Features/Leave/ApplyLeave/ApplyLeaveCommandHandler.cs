using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LeaveApplication = CmScheme.AttendanceLeave.Core.Entities.LeaveApplication;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed class ApplyLeaveCommandHandler(
    IAttendanceLeaveCommandDbContext dbContext,
    INotificationService notificationService,
    IServiceScopeFactory serviceScopeFactory)
    : ICommandHandler<ApplyLeaveCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
    {
        string reportingManagerName = request.ReportingManagerName;

        if (string.IsNullOrWhiteSpace(reportingManagerName))
        {
            reportingManagerName = await ResolveCoordinatorNameAsync(request.ApplicantId, cancellationToken);
        }

        decimal numberOfDays = await CalculateLeaveDaysAsync(request, cancellationToken);

        if (numberOfDays <= 0)
        {
            return Result<int>.Invalid(new ValidationError(
                "The selected range contains no working days — it falls entirely on holidays."));
        }

        LeaveApplication leaveApplication = new LeaveApplication
        {
            ApplicantId = request.ApplicantId,
            LeaveType = request.LeaveType,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            NumberOfDays = numberOfDays,
            HalfDayFullDay = request.HalfDayFullDay,
            LeaveReason = request.LeaveReason,
            AttachmentPath = request.AttachmentPath,
            ReportingManagerName = reportingManagerName,
            Status = Statuses.Leave.Pending,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        dbContext.LeaveApplications.Add(leaveApplication);
        await dbContext.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(reportingManagerName))
        {
            var (subject, body, sms) = NotificationTemplates.LeaveApplied(
                request.CreatedBy,
                request.LeaveType,
                request.NumberOfDays,
                $"{request.FromDate:yyyy-MM-dd} to {request.ToDate:yyyy-MM-dd}");

            await notificationService.SendEmailAsync(
                reportingManagerName,
                subject,
                body,
                cancellationToken);
        }

        return Result<int>.Success(leaveApplication.LeaveApplicationId);
    }

    /// <summary>
    /// Leave duration is a system-derived value: the calendar span excluding declared
    /// holidays. A half-day request always counts as 0.5 of a single day.
    /// </summary>
    private async ValueTask<decimal> CalculateLeaveDaysAsync(
        ApplyLeaveCommand request,
        CancellationToken cancellationToken)
    {
        DateTime from = request.FromDate.Date;
        DateTime to = request.ToDate.Date;

        if (to < from)
        {
            return 0m;
        }

        bool isHalfDay = request.HalfDayFullDay.StartsWith("Half", StringComparison.OrdinalIgnoreCase);

        List<DateTime> holidays = await dbContext.Holidays
            .Where(h => h.IsActive
                        && !h.IsOptional
                        && h.HolidayDate >= from
                        && h.HolidayDate <= to)
            .Select(h => h.HolidayDate)
            .ToListAsync(cancellationToken);

        HashSet<DateTime> holidayDates = holidays.Select(h => h.Date).ToHashSet();

        int workingDays = 0;
        for (DateTime day = from; day <= to; day = day.AddDays(1))
        {
            if (!holidayDates.Contains(day))
            {
                workingDays++;
            }
        }

        if (workingDays == 0)
        {
            return 0m;
        }

        return isHalfDay ? 0.5m : workingDays;
    }

    private async ValueTask<string> ResolveCoordinatorNameAsync(int applicantId, CancellationToken cancellationToken)
    {
        try
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IRegistrationCommandDbContext registrationDbContext = scope.ServiceProvider
                .GetRequiredService<IRegistrationCommandDbContext>();

            UserAccount? applicantAccount = await registrationDbContext.UserAccounts
                .FirstOrDefaultAsync(ua => ua.ApplicantId == applicantId, cancellationToken);

            if (applicantAccount == null)
            {
                return string.Empty;
            }

            int? divisionId = await registrationDbContext.Applicants
                .Where(a => a.ApplicantId == applicantId)
                .Select(a => (int?)a.DivisionId)
                .FirstOrDefaultAsync(cancellationToken);

            if (divisionId.HasValue)
            {
                UserAccount? coordinator = await (
                    from ua in registrationDbContext.UserAccounts
                    join ur in registrationDbContext.UserRoles on ua.UserAccountId equals ur.UserAccountId
                    where ua.IsActive
                          && ur.IsActive
                          && ua.Role == "Coordinator"
                    select ua
                ).FirstOrDefaultAsync(cancellationToken);

                if (coordinator != null)
                {
                    return coordinator.Username;
                }
            }

            UserAccount? fallbackCoordinator = await registrationDbContext.UserAccounts
                .FirstOrDefaultAsync(ua => ua.Role == "Coordinator" && ua.IsActive, cancellationToken);

            return fallbackCoordinator?.Username ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}
