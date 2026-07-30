using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.GetTrainingMaterials;

public sealed record GetTrainingMaterialsQuery : IQuery<Result<List<TrainingMaterialDto>>>
{
    public int TrainingScheduleId { get; init; }
}

public sealed record TrainingMaterialDto
{
    public int TrainingMaterialId { get; init; }
    public int TrainingScheduleId { get; init; }
    public string MaterialName { get; init; } = null!;
    public string FilePath { get; init; } = null!;
    public long FileSize { get; init; }
    public string? ContentType { get; init; }
    public DateTime UploadedOn { get; init; }
    public bool IsActive { get; init; }
}
