using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.Registration.Application.Features.TrainingEnrollment.EnrollTrainee;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.TrainingEnrollment;

public static class Enroll
{
    public static async Task<IResult> Handle(
        EnrollTraineeCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<int> result = await sender.Send(command, cancellationToken);
        return result.ToApiResult();
    }
}
