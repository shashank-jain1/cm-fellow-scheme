using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Core.Data;

public interface IPerformanceQueryDbContext
{
    IQueryable<PerformanceEvaluation> PerformanceEvaluations { get; }
    IQueryable<PerformanceReviewHistory> PerformanceReviewHistories { get; }
    IQueryable<SelfAssessment> SelfAssessments { get; }
    IQueryable<PerformanceReviewCycle> PerformanceReviewCycles { get; }
    IQueryable<PerformanceGoal> PerformanceGoals { get; }
    IQueryable<ImprovementPlan> ImprovementPlans { get; }
    IQueryable<PeerFeedback> PeerFeedbacks { get; }
}
