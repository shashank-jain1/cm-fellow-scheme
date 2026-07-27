using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveBalance;

public sealed class GetLeaveBalanceQueryHandler(IAttendanceLeaveQueryDbContext dbContext)
    : IQueryHandler<GetLeaveBalanceQuery, Result<IReadOnlyList<LeaveBalanceDto>>>
{
    public async ValueTask<Result<IReadOnlyList<LeaveBalanceDto>>> Handle(GetLeaveBalanceQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.LeaveBalance> query = dbContext.LeaveBalances;

        if (request.ApplicantId.HasValue)
        {
            query = query.Where(lb => lb.ApplicantId == request.ApplicantId.Value);
        }

        IReadOnlyList<LeaveBalanceDto> balances = await query
            .Select(lb => new LeaveBalanceDto
            {
                LeaveBalanceId = lb.LeaveBalanceId,
                ApplicantId = lb.ApplicantId,
                LeaveType = lb.LeaveType,
                OpeningBalance = lb.OpeningBalance,
                AvailedLeave = lb.AvailedLeave,
                PendingApprovalLeave = lb.PendingApprovalLeave,
                AvailableBalance = lb.AvailableBalance
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<LeaveBalanceDto>>.Success(balances);
    }
}
