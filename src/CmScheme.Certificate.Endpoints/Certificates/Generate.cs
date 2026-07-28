using Ardalis.Result;
using CmScheme.Certificate.Application.Features.Certificate.GenerateCertificate;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class Generate
{
    public static async Task<IResult> Handle(int certificateId, ISender sender)
    {
        GenerateCertificateCommand command = new GenerateCertificateCommand
        {
            CertificateId = certificateId
        };
        ValueTask<Result<string>> result = sender.Send(command);
        return await result.ToApiResultAsync();
    }
}
