using CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class Apply
{
    public static async Task<IResult> Handle(ApplyForCertificateCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
