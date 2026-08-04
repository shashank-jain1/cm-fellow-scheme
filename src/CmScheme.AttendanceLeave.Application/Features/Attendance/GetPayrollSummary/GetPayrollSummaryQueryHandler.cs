using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;

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

        bool hasPendingLeaves = await dbContext.LeaveApplications
            .AnyAsync(l => l.Status == "Pending", cancellationToken);

        List<PayrollSummaryDto> summaries = await query
            .OrderByDescending(p => p.PayrollMonth)
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
                HasPendingLeaveApprovals = hasPendingLeaves,
                CreatedOn = p.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result.Success(summaries);
    }
}
