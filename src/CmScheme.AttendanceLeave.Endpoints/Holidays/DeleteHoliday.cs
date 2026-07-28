using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Holiday.DeleteHoliday;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Holidays;

public static class DeleteHoliday
{
    public static async Task<IResult> Handle(int holidayId, ISender sender)
    {
        var result = await sender.Send(new DeleteHolidayCommand { HolidayId = holidayId });
        return result.ToApiResult();
    }
}
