using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;

namespace CmScheme.WorkAllocation.Core.Data;

public interface IWorkAllocationQueryDbContext
{
    IQueryable<WorkAllocationEntity> WorkAllocations { get; }
    IQueryable<TaskProgress> TaskProgresses { get; }
    IQueryable<SurveyRecord> SurveyRecords { get; }
    IQueryable<TaskVerification> TaskVerifications { get; }
    IQueryable<TaskAttachment> TaskAttachments { get; }
    IQueryable<TaskDeadline> TaskDeadlines { get; }
}
