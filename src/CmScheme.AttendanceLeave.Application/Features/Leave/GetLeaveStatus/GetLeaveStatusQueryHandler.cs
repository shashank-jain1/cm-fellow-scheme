using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveStatus;

public sealed class GetLeaveStatusQueryHandler(IAttendanceLeaveQueryDbContext dbContext)
    : IQueryHandler<GetLeaveStatusQuery, Result<IReadOnlyList<LeaveStatusDto>>>
{
    public async ValueTask<Result<IReadOnlyList<LeaveStatusDto>>> Handle(GetLeaveStatusQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<LeaveStatusDto> leaves = await dbContext.LeaveApplications
            .Where(l => l.ApplicantId == request.ApplicantId)
            .Select(l => new LeaveStatusDto
            {
                LeaveApplicationId = l.LeaveApplicationId,
                LeaveType = l.LeaveType,
                FromDate = l.FromDate,
                ToDate = l.ToDate,
                NumberOfDays = l.NumberOfDays,
                Status = l.Status,
                CreatedOn = l.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<LeaveStatusDto>>.Success(leaves);
    }
}
