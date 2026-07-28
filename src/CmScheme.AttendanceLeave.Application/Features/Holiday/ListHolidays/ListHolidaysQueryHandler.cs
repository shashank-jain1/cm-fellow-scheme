using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.AttendanceLeave.Core.Data;

namespace CmScheme.AttendanceLeave.Application.Features.Holiday.ListHolidays;

public sealed class ListHolidaysQueryHandler(IAttendanceLeaveCommandDbContext dbContext)
    : IQueryHandler<ListHolidaysQuery, Result<List<HolidayDto>>>
{
    public async ValueTask<Result<List<HolidayDto>>> Handle(ListHolidaysQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.Holiday> query = dbContext.Holidays.Where(h => h.IsActive);

        if (request.Year.HasValue)
        {
            query = query.Where(h => h.HolidayDate.Year == request.Year.Value);
        }

        List<HolidayDto> holidays = await query
            .OrderBy(h => h.HolidayDate)
            .Select(h => new HolidayDto
            {
                HolidayId = h.HolidayId,
                HolidayName = h.HolidayName,
                HolidayDate = h.HolidayDate,
                Description = h.Description,
                IsOptional = h.IsOptional,
                IsActive = h.IsActive
            })
            .ToListAsync(cancellationToken);

        return Result.Success(holidays);
    }
}
