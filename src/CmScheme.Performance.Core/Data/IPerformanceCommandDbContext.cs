using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Core.Data;

public interface IPerformanceCommandDbContext
{
    DbSet<PerformanceEvaluation> PerformanceEvaluations { get; }
    DbSet<PerformanceReviewHistory> PerformanceReviewHistories { get; }
    DbSet<SelfAssessment> SelfAssessments { get; }
    DbSet<PerformanceReviewCycle> PerformanceReviewCycles { get; }
    DbSet<PerformanceGoal> PerformanceGoals { get; }
    DbSet<ImprovementPlan> ImprovementPlans { get; }
    DbSet<PeerFeedback> PeerFeedbacks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
