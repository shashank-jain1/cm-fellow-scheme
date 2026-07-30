using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Training.TrainingMaterial.GetTrainingMaterials;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Trainings;

public static class ListMaterials
{
    public static async Task<IResult> Handle(
        int trainingScheduleId,
        ISender sender)
    {
        GetTrainingMaterialsQuery query = new() { TrainingScheduleId = trainingScheduleId };
        Result<List<TrainingMaterialDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
