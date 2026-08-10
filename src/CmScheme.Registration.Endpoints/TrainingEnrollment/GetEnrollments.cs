using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.Registration.Application.Features.TrainingEnrollment.GetTrainingEnrollments;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.TrainingEnrollment;

public static class GetEnrollments
{
    public static async Task<IResult> Handle(
        int trainingScheduleId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetTrainingEnrollmentsQuery query = new GetTrainingEnrollmentsQuery
        {
            TrainingScheduleId = trainingScheduleId,
        };

        Result<List<TrainingEnrollmentDto>> result = await sender.Send(query, cancellationToken);
        return result.ToApiResult();
    }
}
