using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.Leave.GetLeaveBalance;

public sealed class GetLeaveBalanceQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetLeaveBalanceQuery, Result<List<LeaveBalanceResult>>>
{
    public async ValueTask<Result<List<LeaveBalanceResult>>> Handle(
        GetLeaveBalanceQuery request,
        CancellationToken cancellationToken)
    {
        List<LeaveBalanceResult> result = await dbContext.LeaveBalances
            .Where(lb => lb.UserAccountId == request.UserAccountId && lb.Year == request.Year)
            .OrderBy(lb => lb.LeaveType.SortOrder)
            .Select(lb => new LeaveBalanceResult
            {
                LeaveBalanceId = lb.LeaveBalanceId,
                LeaveTypeName = lb.LeaveType.TypeName,
                LeaveTypeCode = lb.LeaveType.Code,
                TotalDays = lb.TotalDays,
                UsedDays = lb.UsedDays,
                RemainingDays = lb.RemainingDays,
            })
            .ToListAsync(cancellationToken);

        return Result<List<LeaveBalanceResult>>.Success(result);
    }
}
