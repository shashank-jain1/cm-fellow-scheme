using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.DeleteHoliday;

public sealed record DeleteHolidayCommand : ICommand<Result>
{
    public int HolidayId { get; init; }
}
