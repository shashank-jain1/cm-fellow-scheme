using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Application.Features.Training.UploadTrainingMaterial;

public sealed class UploadTrainingMaterialCommandHandler(ITrainingCommandDbContext dbContext)
    : ICommandHandler<UploadTrainingMaterialCommand, Result<string>>
{
    public async ValueTask<Result<string>> Handle(
        UploadTrainingMaterialCommand request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? training = await dbContext.TrainingSchedules
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

        training.MaterialPath = filePath;
        training.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success($"/uploads/training-materials/{uniqueFileName}");
    }
}
