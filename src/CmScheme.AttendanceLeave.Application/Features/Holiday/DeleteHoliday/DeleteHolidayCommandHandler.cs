using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;
using HolidayEntity = CmScheme.AttendanceLeave.Core.Entities.Holiday;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.DeleteHoliday;

public sealed class DeleteHolidayCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<DeleteHolidayCommand, Result>
{
    public async ValueTask<Result> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {
        HolidayEntity? holiday = await dbContext.Holidays
            .FirstOrDefaultAsync(h => h.HolidayId == request.HolidayId, cancellationToken);

        if (holiday is null)
        {
            return Result.NotFound("Holiday not found.");
        }

        holiday.IsActive = false;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
