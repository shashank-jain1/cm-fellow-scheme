using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.Leave.GetLeaveBalance;

public sealed record GetLeaveBalanceQuery : IQuery<Result<List<LeaveBalanceResult>>>
{
    public int UserAccountId { get; init; }
    public int Year { get; init; }
}

public sealed record LeaveBalanceResult
{
    public int LeaveBalanceId { get; init; }
    public string LeaveTypeName { get; init; } = null!;
    public string LeaveTypeCode { get; init; } = null!;
    public decimal TotalDays { get; init; }
    public decimal UsedDays { get; init; }
    public decimal RemainingDays { get; init; }
}

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
