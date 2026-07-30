using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.Registration.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Registration.Application.Features.Leave.CheckConflict;

public sealed class CheckLeaveConflictQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<CheckLeaveConflictQuery, Result<LeaveConflictResult>>
{
    public async ValueTask<Result<LeaveConflictResult>> Handle(
        CheckLeaveConflictQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.LeaveApplication> query = dbContext.LeaveApplications
            .Where(la =>
                la.UserAccountId == request.UserAccountId &&
                la.Status != Statuses.Leave.Cancelled &&
                la.Status != Statuses.Leave.Rejected &&
                la.FromDate <= request.EndDate &&
                la.ToDate >= request.StartDate);

        if (request.ExcludeApplicationId.HasValue)
        {
            query = query.Where(la => la.LeaveApplicationId != request.ExcludeApplicationId.Value);
        }

        List<ConflictingApplicationDto> conflicting = await query
            .Select(la => new ConflictingApplicationDto
            {
                LeaveApplicationId = la.LeaveApplicationId,
                ApplicationNumber = la.ApplicationNumber,
                LeaveTypeName = la.LeaveType.TypeName,
                FromDate = la.FromDate,
                ToDate = la.ToDate,
                NumberOfDays = la.NumberOfDays,
                Status = la.Status,
            })
            .ToListAsync(cancellationToken);

        LeaveConflictResult result = new LeaveConflictResult
        {
            HasConflict = conflicting.Count > 0,
            ConflictingApplications = conflicting,
        };

        return Result<LeaveConflictResult>.Success(result);
    }
}
