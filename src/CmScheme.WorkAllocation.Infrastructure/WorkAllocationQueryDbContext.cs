using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;
using TaskProgressEntity = CmScheme.WorkAllocation.Core.Entities.TaskProgress;
using SurveyRecordEntity = CmScheme.WorkAllocation.Core.Entities.SurveyRecord;
using TaskVerificationEntity = CmScheme.WorkAllocation.Core.Entities.TaskVerification;
using TaskAttachmentEntity = CmScheme.WorkAllocation.Core.Entities.TaskAttachment;
using TaskDeadlineEntity = CmScheme.WorkAllocation.Core.Entities.TaskDeadline;

namespace CmScheme.WorkAllocation.Infrastructure;

public class WorkAllocationQueryDbContext : IWorkAllocationQueryDbContext
{
    private readonly WorkAllocationDbContext _dbContext;

    public WorkAllocationQueryDbContext(WorkAllocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<WorkAllocationEntity> WorkAllocations => _dbContext.WorkAllocations;
    public IQueryable<TaskProgressEntity> TaskProgresses => _dbContext.TaskProgresses;
    public IQueryable<SurveyRecordEntity> SurveyRecords => _dbContext.SurveyRecords;
    public IQueryable<TaskVerificationEntity> TaskVerifications => _dbContext.TaskVerifications;
    public IQueryable<TaskAttachmentEntity> TaskAttachments => _dbContext.TaskAttachments;
    public IQueryable<TaskDeadlineEntity> TaskDeadlines => _dbContext.TaskDeadlines;
    public IQueryable<TaskDependency> TaskDependencies => _dbContext.TaskDependencies;
}
