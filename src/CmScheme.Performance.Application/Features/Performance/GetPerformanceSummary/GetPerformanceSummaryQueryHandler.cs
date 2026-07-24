using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.GetPerformanceSummary;

public sealed class GetPerformanceSummaryQueryHandler(IPerformanceQueryDbContext dbContext)
    : IQueryHandler<GetPerformanceSummaryQuery, Result<PerformanceSummaryDto?>>
{
    public async ValueTask<Result<PerformanceSummaryDto?>> Handle(GetPerformanceSummaryQuery request, CancellationToken cancellationToken)
    {
        PerformanceSummaryDto? summary = await dbContext.PerformanceEvaluations
            .Where(p => p.PerformanceEvaluationId == request.ApplicantId)
            .Select(p => new PerformanceSummaryDto
            {
                PerformanceEvaluationId = p.PerformanceEvaluationId,
                ProjectName = p.ProjectName,
                ApplicantNumber = p.ApplicantNumber,
                ApplicantName = p.ApplicantName,
                TotalSurveysAssigned = p.TotalSurveysAssigned,
                SurveysCompleted = p.SurveysCompleted,
                SurveysPending = p.SurveysPending,
                AttendanceDays = p.AttendanceDays,
                LeaveDays = p.LeaveDays,
                WorkingDays = p.WorkingDays,
                PerformanceScore = p.PerformanceScore,
                PerformanceGrade = p.PerformanceGrade,
                SupervisorRating = p.SupervisorRating,
                QualityScore = p.QualityScore,
                CompletionPercentage = p.CompletionPercentage,
                PerformanceStatus = p.PerformanceStatus,
                EvaluationRemarks = p.EvaluationRemarks
            })
            .FirstOrDefaultAsync(cancellationToken);

        return Result<PerformanceSummaryDto?>.Success(summary);
    }
}
