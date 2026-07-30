using Ardalis.Result;
using CmScheme.Training.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.DownloadTrainingMaterial;

public sealed class DownloadTrainingMaterialQueryHandler(
    ITrainingQueryDbContext dbContext)
    : IQueryHandler<DownloadTrainingMaterialQuery, Result<DownloadTrainingMaterialResult>>
{
    public async ValueTask<Result<DownloadTrainingMaterialResult>> Handle(
        DownloadTrainingMaterialQuery request,
        CancellationToken cancellationToken)
    {
        CmScheme.Training.Core.Entities.TrainingMaterial? material = await dbContext.TrainingMaterials
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.TrainingMaterialId == request.TrainingMaterialId && m.IsActive, cancellationToken);

        if (material is null)
        {
            return Result.NotFound("Training material not found.");
        }

        DownloadTrainingMaterialResult result = new()
        {
            MaterialName = material.MaterialName,
            FilePath = material.FilePath,
            ContentType = material.ContentType
        };

        return Result<DownloadTrainingMaterialResult>.Success(result);
    }
}
