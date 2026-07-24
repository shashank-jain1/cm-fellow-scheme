using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;

namespace CmScheme.WorkAllocation.Core.Data;

public interface IWorkAllocationCommandDbContext
{
    DbSet<WorkAllocationEntity> WorkAllocations { get; }
    DbSet<TaskProgress> TaskProgresses { get; }
    DbSet<SurveyRecord> SurveyRecords { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
