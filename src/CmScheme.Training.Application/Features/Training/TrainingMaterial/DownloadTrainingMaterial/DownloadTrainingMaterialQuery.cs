using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.DownloadTrainingMaterial;

public sealed record DownloadTrainingMaterialQuery : IQuery<Result<DownloadTrainingMaterialResult>>
{
    public int TrainingMaterialId { get; init; }
}

public sealed record DownloadTrainingMaterialResult
{
    public string MaterialName { get; init; } = null!;
    public string FilePath { get; init; } = null!;
    public string? ContentType { get; init; }
}
