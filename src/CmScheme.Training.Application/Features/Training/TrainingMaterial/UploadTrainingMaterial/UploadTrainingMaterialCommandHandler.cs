using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;

namespace CmScheme.Training.Application.Features.Training.TrainingMaterial.UploadTrainingMaterial;

public sealed class UploadTrainingMaterialCommandHandler(
    ITrainingCommandDbContext dbContext)
    : ICommandHandler<UploadTrainingMaterialCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        UploadTrainingMaterialCommand request,
        CancellationToken cancellationToken)
    {
        CmScheme.Training.Core.Entities.TrainingSchedule? training = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(t => t.TrainingScheduleId == request.TrainingScheduleId, cancellationToken);

        if (training is null)
        {
            return Result.NotFound("Training not found.");
        }

        string uploadDir = Path.Combine("wwwroot", "uploads", "training-materials");
        Directory.CreateDirectory(uploadDir);

        string uniqueFileName = $"{request.TrainingScheduleId}_{DateTime.UtcNow:yyyyMMddHHmmss}_{request.FileName}";
        string filePath = Path.Combine(uploadDir, uniqueFileName);

        using FileStream fileStream = new(filePath, FileMode.Create);
        await request.FileStream.CopyToAsync(fileStream, cancellationToken);

        CmScheme.Training.Core.Entities.TrainingMaterial material = new()
        {
            TrainingScheduleId = request.TrainingScheduleId,
            MaterialName = request.MaterialName,
            FilePath = $"/uploads/training-materials/{uniqueFileName}",
            FileSize = request.FileSize,
            ContentType = request.ContentType,
            UploadedOn = DateTime.UtcNow,
            IsActive = true
        };

        dbContext.TrainingMaterials.Add(material);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(material.TrainingMaterialId);
    }
}
