using CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class Review
{
    public static async Task<IResult> Handle(ReviewCertificateCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
