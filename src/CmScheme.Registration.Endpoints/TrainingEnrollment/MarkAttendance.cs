using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.Registration.Application.Features.TrainingEnrollment.MarkTrainingAttendance;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.TrainingEnrollment;

public static class MarkAttendance
{
    public static async Task<IResult> Handle(
        int trainingEnrollmentId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        MarkTrainingAttendanceCommand command = new MarkTrainingAttendanceCommand
        {
            TrainingEnrollmentId = trainingEnrollmentId,
        };

        Result<bool> result = await sender.Send(command, cancellationToken);
        return result.ToApiResult();
    }
}
