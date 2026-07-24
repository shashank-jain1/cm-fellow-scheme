using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.GetSurveyRecordById;

public sealed class GetSurveyRecordByIdQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<GetSurveyRecordByIdQuery, Result<SurveyRecordDto?>>
{
    public async ValueTask<Result<SurveyRecordDto?>> Handle(GetSurveyRecordByIdQuery request, CancellationToken cancellationToken)
    {
        SurveyRecordDto? surveyRecord = await dbContext.SurveyRecords
            .Where(s => s.SurveyRecordId == request.SurveyRecordId)
            .Select(s => new SurveyRecordDto
            {
                SurveyRecordId = s.SurveyRecordId,
                TaskProgressId = s.TaskProgressId,
                InternName = s.InternName,
                SurveyPersonName = s.SurveyPersonName,
                MobileNumber = s.MobileNumber,
                PanchayatName = s.PanchayatName,
                VillageName = s.VillageName,
                SurveyDate = s.SurveyDate,
                SurveyStatus = s.SurveyStatus,
                Latitude = s.Latitude,
                Longitude = s.Longitude
            })
            .FirstOrDefaultAsync(cancellationToken);

        return Result<SurveyRecordDto?>.Success(surveyRecord);
    }
}
