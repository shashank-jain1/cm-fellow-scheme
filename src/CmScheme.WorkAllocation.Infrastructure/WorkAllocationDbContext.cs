using CmScheme.Common.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Data.Configurations;
using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;
using TaskProgressEntity = CmScheme.WorkAllocation.Core.Entities.TaskProgress;
using SurveyRecordEntity = CmScheme.WorkAllocation.Core.Entities.SurveyRecord;

namespace CmScheme.WorkAllocation.Infrastructure;

public class WorkAllocationDbContext : BaseDbContext, IWorkAllocationCommandDbContext, IWorkAllocationQueryDbContext
{
    public DbSet<WorkAllocationEntity> WorkAllocations => Set<WorkAllocationEntity>();
    public DbSet<TaskProgressEntity> TaskProgresses => Set<TaskProgressEntity>();
    public DbSet<SurveyRecordEntity> SurveyRecords => Set<SurveyRecordEntity>();

    public WorkAllocationDbContext(DbContextOptions<WorkAllocationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new WorkAllocationConfiguration());
        modelBuilder.ApplyConfiguration(new TaskProgressConfiguration());
        modelBuilder.ApplyConfiguration(new SurveyRecordConfiguration());
    }

    IQueryable<WorkAllocationEntity> IWorkAllocationQueryDbContext.WorkAllocations => WorkAllocations;
    IQueryable<TaskProgressEntity> IWorkAllocationQueryDbContext.TaskProgresses => TaskProgresses;
    IQueryable<SurveyRecordEntity> IWorkAllocationQueryDbContext.SurveyRecords => SurveyRecords;
}
