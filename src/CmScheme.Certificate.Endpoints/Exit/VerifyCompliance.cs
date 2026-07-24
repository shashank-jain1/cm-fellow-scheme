using CmScheme.Certificate.Application.Features.Exit.VerifyExitCompliance;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Certificate.Endpoints.Exit;

public static class VerifyCompliance
{
    public static async Task<IResult> Handle(VerifyExitComplianceCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
