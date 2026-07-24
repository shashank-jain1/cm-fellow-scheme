using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Core.Data;

public interface IPerformanceQueryDbContext
{
    IQueryable<PerformanceEvaluation> PerformanceEvaluations { get; }
}
