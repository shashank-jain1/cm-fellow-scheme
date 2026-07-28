using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Holiday.UpdateHoliday;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Holidays;

public static class UpdateHoliday
{
    public static async Task<IResult> Handle(int holidayId, UpdateHolidayCommand command, ISender sender)
    {
        var result = await sender.Send(command with { HolidayId = holidayId });
        return result.ToApiResult();
    }
}
