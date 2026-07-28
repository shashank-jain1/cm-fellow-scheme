using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Core.Data;

public interface IPerformanceCommandDbContext
{
    DbSet<PerformanceEvaluation> PerformanceEvaluations { get; }
    DbSet<PerformanceReviewHistory> PerformanceReviewHistories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
