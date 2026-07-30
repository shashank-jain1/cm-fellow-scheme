using Ardalis.Result;
using CmScheme.Training.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.GetTrainingMaterials;

public sealed class GetTrainingMaterialsQueryHandler(
    ITrainingQueryDbContext dbContext)
    : IQueryHandler<GetTrainingMaterialsQuery, Result<List<TrainingMaterialDto>>>
{
    public async ValueTask<Result<List<TrainingMaterialDto>>> Handle(
        GetTrainingMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        List<TrainingMaterialDto> materials = await dbContext.TrainingMaterials
            .AsNoTracking()
            .Where(m => m.TrainingScheduleId == request.TrainingScheduleId && m.IsActive)
            .OrderByDescending(m => m.UploadedOn)
            .Select(m => new TrainingMaterialDto
            {
                TrainingMaterialId = m.TrainingMaterialId,
                TrainingScheduleId = m.TrainingScheduleId,
                MaterialName = m.MaterialName,
                FilePath = m.FilePath,
                FileSize = m.FileSize,
                ContentType = m.ContentType,
                UploadedOn = m.UploadedOn,
                IsActive = m.IsActive
            })
            .ToListAsync(cancellationToken);

        return Result<List<TrainingMaterialDto>>.Success(materials);
    }
}
