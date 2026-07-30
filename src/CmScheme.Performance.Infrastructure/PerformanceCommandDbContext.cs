using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Infrastructure;

public class PerformanceCommandDbContext : IPerformanceCommandDbContext
{
    private readonly PerformanceDbContext _dbContext;

    public PerformanceCommandDbContext(PerformanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<PerformanceEvaluation> PerformanceEvaluations => _dbContext.PerformanceEvaluations;
    public DbSet<PerformanceReviewHistory> PerformanceReviewHistories => _dbContext.PerformanceReviewHistories;
    public DbSet<SelfAssessment> SelfAssessments => _dbContext.SelfAssessments;
    public DbSet<PerformanceReviewCycle> PerformanceReviewCycles => _dbContext.PerformanceReviewCycles;
    public DbSet<PerformanceGoal> PerformanceGoals => _dbContext.PerformanceGoals;
    public DbSet<ImprovementPlan> ImprovementPlans => _dbContext.ImprovementPlans;
    public DbSet<PeerFeedback> PeerFeedbacks => _dbContext.PeerFeedbacks;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
