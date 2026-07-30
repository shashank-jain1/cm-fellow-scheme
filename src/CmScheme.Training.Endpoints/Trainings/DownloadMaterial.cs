using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Training.Application.Features.Training.TrainingMaterial.DownloadTrainingMaterial;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Training.Endpoints.Trainings;

public static class DownloadMaterial
{
    public static async Task<IResult> Handle(
        int trainingMaterialId,
        ISender sender)
    {
        DownloadTrainingMaterialQuery query = new() { TrainingMaterialId = trainingMaterialId };
        Result<DownloadTrainingMaterialResult> result = await sender.Send(query);

        if (!result.IsSuccess)
        {
            return result.ToApiResult();
        }

        DownloadTrainingMaterialResult material = result.Value;
        string fullPath = Path.Combine("wwwroot", material.FilePath.TrimStart('/'));

        if (!System.IO.File.Exists(fullPath))
        {
            return Results.NotFound("File not found on disk.");
        }

        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
        return Results.File(fileBytes, material.ContentType ?? "application/octet-stream", material.MaterialName);
    }
}
