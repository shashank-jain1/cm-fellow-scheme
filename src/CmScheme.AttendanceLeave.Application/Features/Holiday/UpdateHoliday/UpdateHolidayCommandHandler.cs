using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;
using HolidayEntity = CmScheme.AttendanceLeave.Core.Entities.Holiday;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.UpdateHoliday;

public sealed class UpdateHolidayCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<UpdateHolidayCommand, Result>
{
    public async ValueTask<Result> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
    {
        HolidayEntity? holiday = await dbContext.Holidays
            .FirstOrDefaultAsync(h => h.HolidayId == request.HolidayId, cancellationToken);

        if (holiday is null)
        {
            return Result.NotFound("Holiday not found.");
        }

        holiday.HolidayName = request.HolidayName;
        holiday.HolidayDate = request.HolidayDate;
        holiday.Description = request.Description;
        holiday.IsOptional = request.IsOptional;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
