using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.CheckOutAttendance;

public sealed class CheckOutAttendanceCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<CheckOutAttendanceCommand, Result>
{
    public async ValueTask<Result> Handle(
        CheckOutAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.Attendance? attendance = await dbContext.Attendances
            .FirstOrDefaultAsync(a =>
                a.ApplicantId == request.ApplicantId &&
                a.AttendanceDate.Date == request.AttendanceDate.Date,
                cancellationToken);

        if (attendance is null)
        {
            return Result.NotFound("No attendance record found for today.");
        }

        if (attendance.CheckOutTime.HasValue)
        {
            return Result.Invalid(new ValidationError("Already checked out for today."));
        }

        attendance.CheckOutTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        attendance.AttendanceStatus = Statuses.Attendance.CheckedOut;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
