using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.UploadTrainingMaterial;

public sealed record UploadTrainingMaterialCommand : ICommand<Result<int>>
{
    public int TrainingScheduleId { get; init; }
    public string MaterialName { get; init; } = null!;
    public string FileName { get; init; } = null!;
    public Stream FileStream { get; init; } = null!;
    public string ContentType { get; init; } = null!;
    public long FileSize { get; init; }
}
