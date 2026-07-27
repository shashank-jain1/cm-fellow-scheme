using CmScheme.Certificate.Application.Features.Certificate.ListCertificates;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class List
{
    public static async Task<IResult> Handle(ISender sender)
    {
        var result = await sender.Send(new ListCertificatesQuery());
        return result.ToApiResult();
    }
}
