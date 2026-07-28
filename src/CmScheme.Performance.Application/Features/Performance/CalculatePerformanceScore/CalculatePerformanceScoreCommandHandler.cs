using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;

namespace CmScheme.Performance.Application.Features.Performance.CalculatePerformanceScore;

public sealed class CalculatePerformanceScoreCommandHandler(IPerformanceCommandDbContext dbContext)
    : ICommandHandler<CalculatePerformanceScoreCommand, Result>
{
    public async ValueTask<Result> Handle(
        CalculatePerformanceScoreCommand request,
        CancellationToken cancellationToken)
    {
        PerformanceEvaluation? evaluation = await dbContext.PerformanceEvaluations
            .FirstOrDefaultAsync(pe => pe.PerformanceEvaluationId == request.PerformanceEvaluationId, cancellationToken);

        if (evaluation is null)
        {
            return Result.NotFound("Performance evaluation not found.");
        }

        decimal completionScore = evaluation.TotalSurveysAssigned > 0
            ? (decimal)evaluation.SurveysCompleted / evaluation.TotalSurveysAssigned * 40
            : 0;

        decimal attendanceScore = evaluation.WorkingDays > 0
            ? (decimal)evaluation.AttendanceDays / evaluation.WorkingDays * 30
            : 0;

        decimal qualityScore = evaluation.ApprovedSurveys > 0
            ? (decimal)evaluation.ApprovedSurveys / (evaluation.ApprovedSurveys + evaluation.RejectedSurveys) * 30
            : 0;

        evaluation.PerformanceScore = Math.Round(completionScore + attendanceScore + qualityScore, 2);
        evaluation.QualityScore = Math.Round(qualityScore * 100 / 30, 2);
        evaluation.CompletionPercentage = evaluation.TotalSurveysAssigned > 0
            ? Math.Round((decimal)evaluation.SurveysCompleted / evaluation.TotalSurveysAssigned * 100, 2)
            : 0;

        evaluation.PerformanceGrade = evaluation.PerformanceScore switch
        {
            >= 90 => "A+",
            >= 80 => "A",
            >= 70 => "B+",
            >= 60 => "B",
            >= 50 => "C",
            _ => "D"
        };

        evaluation.PerformanceStatus = evaluation.PerformanceScore switch
        {
            >= 80 => "Excellent",
            >= 60 => "Good",
            >= 40 => "Average",
            _ => "Poor"
        };

        evaluation.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
