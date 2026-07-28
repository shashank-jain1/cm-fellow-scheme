using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.RecordSurveySubmission;

public sealed class RecordSurveySubmissionCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<RecordSurveySubmissionCommand, Result>
{
    public async ValueTask<Result> Handle(
        RecordSurveySubmissionCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.TaskProgress? taskProgress = await dbContext.TaskProgresses
            .FirstOrDefaultAsync(tp => tp.TaskProgressId == request.TaskProgressId, cancellationToken);

        if (taskProgress is null)
        {
            return Result.NotFound("Task progress not found.");
        }

        Core.Entities.SurveyRecord surveyRecord = new Core.Entities.SurveyRecord
        {
            TaskProgressId = request.TaskProgressId,
            InternName = request.ApplicantId.ToString(),
            SurveyPersonName = request.SurveyPersonName,
            MobileNumber = request.MobileNumber,
            PanchayatName = request.PanchayatName,
            VillageName = request.VillageName,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            SurveyDate = DateTime.UtcNow,
            SurveyStatus = "Submitted"
        };

        dbContext.SurveyRecords.Add(surveyRecord);

        taskProgress.CompletedSurveys++;
        taskProgress.CompletionPercentage = taskProgress.NumberOfSurveys > 0
            ? (decimal)taskProgress.CompletedSurveys / taskProgress.NumberOfSurveys * 100
            : 0;

        if (taskProgress.CompletedSurveys >= taskProgress.NumberOfSurveys)
        {
            taskProgress.WorkStatus = "Completed";
            taskProgress.CompletionDate = DateTime.UtcNow;
        }
        else if (taskProgress.CompletedSurveys > 0)
        {
            taskProgress.WorkStatus = "In Progress";
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
