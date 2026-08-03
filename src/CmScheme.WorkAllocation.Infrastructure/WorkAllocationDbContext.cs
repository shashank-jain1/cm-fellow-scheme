using CmScheme.Common.Core.Data;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Data.Configurations;
using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;
using TaskProgressEntity = CmScheme.WorkAllocation.Core.Entities.TaskProgress;
using SurveyRecordEntity = CmScheme.WorkAllocation.Core.Entities.SurveyRecord;
using TaskVerificationEntity = CmScheme.WorkAllocation.Core.Entities.TaskVerification;
using TaskAttachmentEntity = CmScheme.WorkAllocation.Core.Entities.TaskAttachment;
using TaskDeadlineEntity = CmScheme.WorkAllocation.Core.Entities.TaskDeadline;
using TaskAssignmentEntity = CmScheme.WorkAllocation.Core.Entities.TaskAssignment;

namespace CmScheme.WorkAllocation.Infrastructure;

public class WorkAllocationDbContext : BaseDbContext, IWorkAllocationCommandDbContext, IWorkAllocationQueryDbContext
{
    public DbSet<WorkAllocationEntity> WorkAllocations => Set<WorkAllocationEntity>();
    public DbSet<TaskProgressEntity> TaskProgresses => Set<TaskProgressEntity>();
    public DbSet<SurveyRecordEntity> SurveyRecords => Set<SurveyRecordEntity>();
    public DbSet<TaskVerificationEntity> TaskVerifications => Set<TaskVerificationEntity>();
    public DbSet<TaskAttachmentEntity> TaskAttachments => Set<TaskAttachmentEntity>();
    public DbSet<TaskDeadlineEntity> TaskDeadlines => Set<TaskDeadlineEntity>();
    public DbSet<TaskDependency> TaskDependencies => Set<TaskDependency>();
    public DbSet<TaskAssignmentEntity> TaskAssignments => Set<TaskAssignmentEntity>();

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
        modelBuilder.ApplyConfiguration(new TaskVerificationConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new TaskDeadlineConfiguration());
        modelBuilder.ApplyConfiguration(new TaskDependencyConfiguration());
    }

    IQueryable<WorkAllocationEntity> IWorkAllocationQueryDbContext.WorkAllocations => WorkAllocations;
    IQueryable<TaskProgressEntity> IWorkAllocationQueryDbContext.TaskProgresses => TaskProgresses;
    IQueryable<SurveyRecordEntity> IWorkAllocationQueryDbContext.SurveyRecords => SurveyRecords;
    IQueryable<TaskVerificationEntity> IWorkAllocationQueryDbContext.TaskVerifications => TaskVerifications;
    IQueryable<TaskAttachmentEntity> IWorkAllocationQueryDbContext.TaskAttachments => TaskAttachments;
    IQueryable<TaskDeadlineEntity> IWorkAllocationQueryDbContext.TaskDeadlines => TaskDeadlines;
    IQueryable<TaskDependency> IWorkAllocationQueryDbContext.TaskDependencies => TaskDependencies;
}
