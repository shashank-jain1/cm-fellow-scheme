using CmScheme.Common.Core.Data;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Data.Configurations;
using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Infrastructure;

public class PerformanceDbContext : BaseDbContext, IPerformanceCommandDbContext, IPerformanceQueryDbContext
{
    public DbSet<PerformanceEvaluation> PerformanceEvaluations => Set<PerformanceEvaluation>();
    public DbSet<PerformanceReviewHistory> PerformanceReviewHistories => Set<PerformanceReviewHistory>();
    public DbSet<SelfAssessment> SelfAssessments => Set<SelfAssessment>();
    public DbSet<PerformanceReviewCycle> PerformanceReviewCycles => Set<PerformanceReviewCycle>();
    public DbSet<PerformanceGoal> PerformanceGoals => Set<PerformanceGoal>();
    public DbSet<ImprovementPlan> ImprovementPlans => Set<ImprovementPlan>();
    public DbSet<PeerFeedback> PeerFeedbacks => Set<PeerFeedback>();

    public PerformanceDbContext(DbContextOptions<PerformanceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PerformanceEvaluationConfiguration());
        modelBuilder.ApplyConfiguration(new PerformanceReviewHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new SelfAssessmentConfiguration());
        modelBuilder.ApplyConfiguration(new PerformanceReviewCycleConfiguration());
        modelBuilder.ApplyConfiguration(new PerformanceGoalConfiguration());
        modelBuilder.ApplyConfiguration(new ImprovementPlanConfiguration());
        modelBuilder.ApplyConfiguration(new PeerFeedbackConfiguration());
    }

    IQueryable<PerformanceEvaluation> IPerformanceQueryDbContext.PerformanceEvaluations => PerformanceEvaluations;
    IQueryable<PerformanceReviewHistory> IPerformanceQueryDbContext.PerformanceReviewHistories => PerformanceReviewHistories;
    IQueryable<SelfAssessment> IPerformanceQueryDbContext.SelfAssessments => SelfAssessments;
    IQueryable<PerformanceReviewCycle> IPerformanceQueryDbContext.PerformanceReviewCycles => PerformanceReviewCycles;
    IQueryable<PerformanceGoal> IPerformanceQueryDbContext.PerformanceGoals => PerformanceGoals;
    IQueryable<ImprovementPlan> IPerformanceQueryDbContext.ImprovementPlans => ImprovementPlans;
    IQueryable<PeerFeedback> IPerformanceQueryDbContext.PeerFeedbacks => PeerFeedbacks;
}
