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

    public PerformanceDbContext(DbContextOptions<PerformanceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PerformanceEvaluationConfiguration());
        modelBuilder.ApplyConfiguration(new PerformanceReviewHistoryConfiguration());
    }

    IQueryable<PerformanceEvaluation> IPerformanceQueryDbContext.PerformanceEvaluations => PerformanceEvaluations;
    IQueryable<PerformanceReviewHistory> IPerformanceQueryDbContext.PerformanceReviewHistories => PerformanceReviewHistories;
}
