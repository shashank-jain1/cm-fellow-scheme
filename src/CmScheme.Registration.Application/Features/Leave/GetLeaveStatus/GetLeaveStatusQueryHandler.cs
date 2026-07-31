using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.Leave.GetLeaveStatus;

public sealed class GetLeaveStatusQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetLeaveStatusQuery, Result<List<LeaveStatusResult>>>
{
    public async ValueTask<Result<List<LeaveStatusResult>>> Handle(
        GetLeaveStatusQuery request,
        CancellationToken cancellationToken)
    {
        List<LeaveStatusResult> result = await dbContext.LeaveApplications
            .Where(la => la.UserAccountId == request.UserAccountId)
            .OrderByDescending(la => la.CreatedOn)
            .Select(la => new LeaveStatusResult
            {
                LeaveApplicationId = la.LeaveApplicationId,
                ApplicationNumber = la.ApplicationNumber,
                LeaveTypeName = la.LeaveType.TypeName,
                FromDate = la.FromDate,
                ToDate = la.ToDate,
                NumberOfDays = la.NumberOfDays,
                IsHalfDay = la.IsHalfDay,
                Reason = la.Reason,
                Status = la.Status,
                ApprovalRemarks = la.ApprovalRemarks,
                ApprovalDate = la.ApprovalDate,
                CreatedOn = la.CreatedOn,
            })
            .ToListAsync(cancellationToken);

        return Result<List<LeaveStatusResult>>.Success(result);
    }
}
