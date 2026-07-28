using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.CreateHoliday;

public sealed record CreateHolidayCommand : ICommand<Result<int>>
{
    public string HolidayName { get; init; } = null!;
    public DateTime HolidayDate { get; init; }
    public string? Description { get; init; }
    public bool IsOptional { get; init; }
}
