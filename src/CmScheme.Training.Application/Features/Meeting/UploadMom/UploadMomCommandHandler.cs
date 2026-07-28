using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Application.Features.Meeting.UploadMom;

public sealed class UploadMomCommandHandler(ITrainingCommandDbContext dbContext)
    : ICommandHandler<UploadMomCommand, Result<string>>
{
    public async ValueTask<Result<string>> Handle(
        UploadMomCommand request,
        CancellationToken cancellationToken)
    {
        TrainingSchedule? meeting = await dbContext.TrainingSchedules
            .FirstOrDefaultAsync(t => t.TrainingScheduleId == request.TrainingScheduleId, cancellationToken);

        if (meeting is null)
        {
            return Result.NotFound("Meeting not found.");
        }

        string uploadDir = Path.Combine("wwwroot", "uploads", "meeting-mom");
        Directory.CreateDirectory(uploadDir);

        string uniqueFileName = $"{request.TrainingScheduleId}_{DateTime.UtcNow:yyyyMMddHHmmss}_{request.FileName}";
        string filePath = Path.Combine(uploadDir, uniqueFileName);

        using FileStream fileStream = new(filePath, FileMode.Create);
        await request.FileStream.CopyToAsync(fileStream, cancellationToken);

        meeting.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success($"/uploads/meeting-mom/{uniqueFileName}");
    }
}
