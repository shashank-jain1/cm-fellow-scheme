using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Training.TrainingMaterial.UploadTrainingMaterial;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Trainings;

public static class UploadMaterial
{
    public static async Task<IResult> Handle(
        int trainingScheduleId,
        IFormFile file,
        string materialName,
        IMediator mediator,
        CancellationToken ct)
    {
        UploadTrainingMaterialCommand command = new()
        {
            TrainingScheduleId = trainingScheduleId,
            MaterialName = materialName,
            FileName = file.FileName,
            FileStream = file.OpenReadStream(),
            ContentType = file.ContentType,
            FileSize = file.Length
        };
        Result<int> result = await mediator.Send(command, ct);
        return result.ToApiResult();
    }
}
