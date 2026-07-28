using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Holiday.CreateHoliday;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Holidays;

public static class CreateHoliday
{
    public static async Task<IResult> Handle(CreateHolidayCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
