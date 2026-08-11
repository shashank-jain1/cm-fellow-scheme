using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetPayrollSummary;

public sealed class GetPayrollSummaryQueryHandler(IAttendanceLeaveCommandDbContext dbContext)
    : IQueryHandler<GetPayrollSummaryQuery, Result<List<PayrollSummaryDto>>>
{
    public async ValueTask<Result<List<PayrollSummaryDto>>> Handle(
        GetPayrollSummaryQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.PayrollAttendanceSummary> query = dbContext.PayrollAttendanceSummaries;

        if (!string.IsNullOrEmpty(request.PayrollMonth))
        {
            query = query.Where(p => p.PayrollMonth == request.PayrollMonth);
        }

        if (request.ApplicantId.HasValue)
        {
            query = query.Where(p => p.ApplicantId == request.ApplicantId.Value);
        }

        List<Core.Entities.PayrollAttendanceSummary> rows = await query
            .OrderByDescending(p => p.PayrollMonth)
            .ToListAsync(cancellationToken);

        // "Attendance processing cannot be finalised until leave approvals for the attendance
        // month are completed" — so the block must be evaluated per applicant and per month,
        // not as one global flag over every pending leave in the system.
        HashSet<(int ApplicantId, string PayrollMonth)> blocked = [];

        foreach (IGrouping<string, Core.Entities.PayrollAttendanceSummary> monthGroup in rows.GroupBy(r => r.PayrollMonth))
        {
            if (!TryParsePayrollMonth(monthGroup.Key, out DateTime monthStart))
            {
                continue;
            }

            DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
            int[] applicantIds = monthGroup.Select(r => r.ApplicantId).Distinct().ToArray();

            List<int> pendingApplicants = await dbContext.LeaveApplications
                .Where(l => l.Status == Statuses.Leave.Pending
                            && applicantIds.Contains(l.ApplicantId)
                            && l.FromDate <= monthEnd
                            && l.ToDate >= monthStart)
                .Select(l => l.ApplicantId)
                .Distinct()
                .ToListAsync(cancellationToken);

            foreach (int applicantId in pendingApplicants)
            {
                blocked.Add((applicantId, monthGroup.Key));
            }
        }

        List<PayrollSummaryDto> summaries = rows
            .Select(p => new PayrollSummaryDto
            {
                PayrollAttendanceSummaryId = p.PayrollAttendanceSummaryId,
                ApplicantId = p.ApplicantId,
                PayrollMonth = p.PayrollMonth,
                TotalWorkingDays = p.TotalWorkingDays,
                PresentDays = p.PresentDays,
                ApprovedLeaveDays = p.ApprovedLeaveDays,
                AbsentDays = p.AbsentDays,
                PayableDays = p.PayableDays,
                HasPendingLeaveApprovals = blocked.Contains((p.ApplicantId, p.PayrollMonth)),
                CreatedOn = p.CreatedOn
            })
            .ToList();

        return Result.Success(summaries);
    }

    /// <summary>Payroll month is stored as "yyyy-MM" (e.g. "2026-07").</summary>
    private static bool TryParsePayrollMonth(string payrollMonth, out DateTime monthStart)
    {
        return DateTime.TryParseExact(
            payrollMonth,
            "yyyy-MM",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out monthStart);
    }
}
