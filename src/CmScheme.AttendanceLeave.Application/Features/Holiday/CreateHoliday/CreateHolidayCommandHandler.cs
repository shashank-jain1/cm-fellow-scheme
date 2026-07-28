using Ardalis.Result;
using Mediator;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using HolidayEntity = CmScheme.AttendanceLeave.Core.Entities.Holiday;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.CreateHoliday;

public sealed class CreateHolidayCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<CreateHolidayCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        HolidayEntity holiday = new HolidayEntity
        {
            HolidayName = request.HolidayName,
            HolidayDate = request.HolidayDate,
            Description = request.Description,
            IsOptional = request.IsOptional,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.Holidays.Add(holiday);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(holiday.HolidayId);
    }
}
