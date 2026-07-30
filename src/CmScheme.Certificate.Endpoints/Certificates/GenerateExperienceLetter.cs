using Ardalis.Result;
using CmScheme.Certificate.Application.Features.Certificates.GenerateExperienceLetter;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class GenerateExperienceLetter
{
    public static async Task<IResult> Handle(
        int applicantId,
        DateTime startDate,
        DateTime endDate,
        string supervisorName,
        ISender sender)
    {
        GenerateExperienceLetterCommand command = new GenerateExperienceLetterCommand
        {
            ApplicantId = applicantId,
            StartDate = startDate,
            EndDate = endDate,
            SupervisorName = supervisorName
        };
        ValueTask<Result<string>> result = sender.Send(command);
        return await result.ToApiResultAsync();
    }
}
