using Ardalis.Result;
using CmScheme.Certificate.Application.Features.Certificates.VerifyCertificate;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class VerifyCertificate
{
    public static async Task<IResult> Handle(string certificateNumber, ISender sender)
    {
        VerifyCertificateQuery query = new VerifyCertificateQuery
        {
            CertificateNumber = certificateNumber
        };
        ValueTask<Result<CertificateVerificationResult>> result = sender.Send(query);
        return await result.ToApiResultAsync();
    }
}
