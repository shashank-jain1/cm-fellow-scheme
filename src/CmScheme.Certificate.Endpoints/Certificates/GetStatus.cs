using CmScheme.Certificate.Application.Features.Certificate.GetCertificateStatus;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class GetStatus
{
    public static async Task<IResult> Handle([AsParameters] GetCertificateStatusQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
