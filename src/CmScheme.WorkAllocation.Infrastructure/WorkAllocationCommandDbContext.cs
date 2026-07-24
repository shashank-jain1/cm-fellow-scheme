using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;
using TaskProgressEntity = CmScheme.WorkAllocation.Core.Entities.TaskProgress;
using SurveyRecordEntity = CmScheme.WorkAllocation.Core.Entities.SurveyRecord;

namespace CmScheme.WorkAllocation.Infrastructure;

public class WorkAllocationCommandDbContext : IWorkAllocationCommandDbContext
{
    private readonly WorkAllocationDbContext _dbContext;

    public WorkAllocationCommandDbContext(WorkAllocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<WorkAllocationEntity> WorkAllocations => _dbContext.WorkAllocations;
    public DbSet<TaskProgressEntity> TaskProgresses => _dbContext.TaskProgresses;
    public DbSet<SurveyRecordEntity> SurveyRecords => _dbContext.SurveyRecords;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
