using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;
using SurveyRecordEntity = CmScheme.WorkAllocation.Core.Entities.SurveyRecord;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.CreateSurveyRecord;

public sealed class CreateSurveyRecordCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<CreateSurveyRecordCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateSurveyRecordCommand request, CancellationToken cancellationToken)
    {
        SurveyRecordEntity surveyRecord = new SurveyRecordEntity
        {
            TaskProgressId = request.TaskProgressId,
            InternName = request.InternName,
            SurveyPersonName = request.SurveyPersonName,
            MobileNumber = request.MobileNumber,
            PanchayatName = request.PanchayatName,
            VillageName = request.VillageName,
            SurveyDate = request.SurveyDate,
            SurveyStatus = request.SurveyStatus,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        dbContext.SurveyRecords.Add(surveyRecord);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(surveyRecord.SurveyRecordId);
    }
}
