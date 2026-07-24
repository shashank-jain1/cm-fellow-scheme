using CmScheme.Performance.Application.Features.Performance.RecordEvaluationRemarks;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Performance.Endpoints.Performance;

public static class RecordRemarks
{
    public static async Task<IResult> Handle(RecordEvaluationRemarksCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
