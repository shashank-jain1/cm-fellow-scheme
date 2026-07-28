using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.UpdateHoliday;

public sealed record UpdateHolidayCommand : ICommand<Result>
{
    public int HolidayId { get; init; }
    public string HolidayName { get; init; } = null!;
    public DateTime HolidayDate { get; init; }
    public string? Description { get; init; }
    public bool IsOptional { get; init; }
}
