using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.ListSurveyRecords;

public sealed class ListSurveyRecordsQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<ListSurveyRecordsQuery, Result<IReadOnlyList<SurveyRecordDto>>>
{
    public async ValueTask<Result<IReadOnlyList<SurveyRecordDto>>> Handle(ListSurveyRecordsQuery request, CancellationToken cancellationToken)
    {
        List<SurveyRecordDto> surveyRecords = await dbContext.SurveyRecords
            .Where(s => s.TaskProgressId == request.TaskProgressId)
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
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<SurveyRecordDto>>.Success(surveyRecords);
    }
}
