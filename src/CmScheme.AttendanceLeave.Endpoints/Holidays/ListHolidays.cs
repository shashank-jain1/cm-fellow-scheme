using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Holiday.ListHolidays;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Holidays;

public static class ListHolidays
{
    public static async Task<IResult> Handle(int? year, ISender sender)
    {
        var result = await sender.Send(new ListHolidaysQuery { Year = year });
        return result.ToApiResult();
    }
}
