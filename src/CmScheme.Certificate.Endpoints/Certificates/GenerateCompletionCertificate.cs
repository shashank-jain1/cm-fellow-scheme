using Ardalis.Result;
using CmScheme.Certificate.Application.Features.Certificates.GenerateCompletionCertificate;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class GenerateCompletionCertificate
{
    public static async Task<IResult> Handle(int applicantId, ISender sender)
    {
        GenerateCompletionCertificateCommand command = new GenerateCompletionCertificateCommand
        {
            ApplicantId = applicantId
        };
        ValueTask<Result<string>> result = sender.Send(command);
        return await result.ToApiResultAsync();
    }
}
