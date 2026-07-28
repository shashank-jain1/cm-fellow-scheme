using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.ListHolidays;

public sealed record ListHolidaysQuery : IQuery<Result<List<HolidayDto>>>
{
    public int? Year { get; init; }
}

public sealed record HolidayDto
{
    public int HolidayId { get; init; }
    public string HolidayName { get; init; } = null!;
    public DateTime HolidayDate { get; init; }
    public string? Description { get; init; }
    public bool IsOptional { get; init; }
    public bool IsActive { get; init; }
}
