using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;

namespace CmScheme.Performance.Application.Features.Performance.CalculatePerformanceScore;

public sealed class CalculatePerformanceScoreCommandHandler(
    IPerformanceCommandDbContext dbContext,
    IPerformanceQueryDbContext queryDbContext)
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

        // Check if real attendance data exists via AttendanceViews
        if (evaluation.AttendanceDays == 0 && !string.IsNullOrEmpty(evaluation.ApplicantName))
        {
            int realAttendance = await queryDbContext.AttendanceViews
                .Where(a => a.Status == "Present")
                .CountAsync(cancellationToken);

            if (realAttendance > 0)
            {
                evaluation.AttendanceDays = realAttendance;
                if (evaluation.WorkingDays == 0) evaluation.WorkingDays = 30;
            }
        }

        // Check if real survey progress data exists via TaskProgressViews / WorkAllocationViews
        if (evaluation.SurveysCompleted == 0)
        {
            int totalCompleted = await queryDbContext.TaskProgressViews
                .SumAsync(tp => (int?)tp.CompletedSurveys, cancellationToken) ?? 0;

            int totalTarget = await queryDbContext.TaskProgressViews
                .SumAsync(tp => (int?)tp.TargetSurveys, cancellationToken) ?? 0;

            if (totalCompleted > 0)
            {
                evaluation.SurveysCompleted = totalCompleted;
                if (evaluation.TotalSurveysAssigned == 0)
                {
                    evaluation.TotalSurveysAssigned = totalTarget > 0 ? totalTarget : totalCompleted;
                }
            }
        }

        // Survey completion rate (40%): completed / assigned
        decimal surveyCompletionRate = evaluation.TotalSurveysAssigned > 0
            ? (decimal)evaluation.SurveysCompleted / evaluation.TotalSurveysAssigned
            : 0;
        decimal surveyCompletionScore = surveyCompletionRate * 40;

        // Attendance percentage (30%): attendance days / working days
        decimal attendanceRate = evaluation.WorkingDays > 0
            ? (decimal)evaluation.AttendanceDays / evaluation.WorkingDays
            : 0;
        decimal attendanceScore = attendanceRate * 30;

        // Quality score (20%): approved surveys / total surveys
        int totalSurveys = evaluation.ApprovedSurveys + evaluation.RejectedSurveys;
        decimal qualityRate = totalSurveys > 0
            ? (decimal)evaluation.ApprovedSurveys / totalSurveys
            : 0;
        decimal qualityScore = qualityRate * 20;

        // Supervisor rating (10%): rating 1-5 scaled to 100
        decimal supervisorScore = 0;
        if (evaluation.SupervisorRating.HasValue && evaluation.SupervisorRating > 0)
        {
            decimal scaledRating = Math.Clamp(evaluation.SupervisorRating.Value, 1, 5);
            supervisorScore = (scaledRating / 5) * 100 * 0.10m;
        }

        // Compute final score (0-100)
        evaluation.PerformanceScore = Math.Round(
            surveyCompletionScore + attendanceScore + qualityScore + supervisorScore, 2);

        // Store quality score as percentage (0-100)
        evaluation.QualityScore = Math.Round(qualityRate * 100, 2);

        // Store completion percentage (0-100)
        evaluation.CompletionPercentage = Math.Round(surveyCompletionRate * 100, 2);

        // Assign grade
        evaluation.PerformanceGrade = evaluation.PerformanceScore switch
        {
            >= 90 => "A+",
            >= 80 => "A",
            >= 70 => "B+",
            >= 60 => "B",
            >= 50 => "C",
            _ => "D"
        };

        // Assign status
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
