using CmScheme.Certificate.Application.Features.Exit.SubmitExitReadiness;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Certificate.Endpoints.Exit;

public static class SubmitReadiness
{
    public static async Task<IResult> Handle(SubmitExitReadinessCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
