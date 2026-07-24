using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Infrastructure;

public class PerformanceQueryDbContext : IPerformanceQueryDbContext
{
    private readonly PerformanceDbContext _dbContext;

    public PerformanceQueryDbContext(PerformanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<PerformanceEvaluation> PerformanceEvaluations => _dbContext.PerformanceEvaluations;
}
