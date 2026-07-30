using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Training.Application.Features.Training.UpdateTrainingStatus;
using CmScheme.Training.Application.Features.Training.TrainingMaterial.GetTrainingMaterials;
using CmScheme.Endpoints.Abstractions.Extensions;

namespace CmScheme.Training.Endpoints.Trainings;

public sealed class TrainingEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapTrainingGroup();

        group.MapGet("", ListTrainings.List)
            .WithTags("Training")
            .WithName("ListTrainings")
            .WithDisplayName("List training sessions");

        group.MapPost("", CreateTraining.Create)
            .WithTags("Training")
            .WithName("CreateTraining")
            .WithDisplayName("Create a training session");

        group.MapPut("/{trainingScheduleId:int}/status", async (
            int trainingScheduleId,
            UpdateTrainingStatusRequest request,
            IMediator mediator,
            CancellationToken ct) =>
        {
            UpdateTrainingStatusCommand command = new()
            {
                TrainingScheduleId = trainingScheduleId,
                NewStatus = request.NewStatus
            };
            Ardalis.Result.Result result = await mediator.Send(command, ct);
            return result.ToApiResult();
        })
        .WithName("UpdateTrainingStatus")
        .WithDisplayName("Update training status")
        .WithTags("Training")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/{trainingScheduleId:int}/materials", UploadMaterial.Handle)
            .WithName("UploadTrainingMaterial")
            .WithDisplayName("Upload training material")
            .WithTags("Training Materials")
            .DisableAntiforgery()
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/{trainingScheduleId:int}/materials", ListMaterials.Handle)
            .WithName("GetTrainingMaterials")
            .WithDisplayName("List training materials")
            .WithTags("Training Materials")
            .Produces<List<TrainingMaterialDto>>();

        group.MapGet("/materials/{trainingMaterialId:int}/download", DownloadMaterial.Handle)
            .WithName("DownloadTrainingMaterial")
            .WithDisplayName("Download training material")
            .WithTags("Training Materials")
            .Produces<FileResult>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

public sealed record UpdateTrainingStatusRequest(string NewStatus);
