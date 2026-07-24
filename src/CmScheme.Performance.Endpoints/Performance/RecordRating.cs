using CmScheme.Performance.Application.Features.Performance.RecordSupervisorRating;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Performance.Endpoints.Performance;

public static class RecordRating
{
    public static async Task<IResult> Handle(RecordSupervisorRatingCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
