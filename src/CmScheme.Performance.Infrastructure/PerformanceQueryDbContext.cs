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
    public IQueryable<PerformanceReviewHistory> PerformanceReviewHistories => _dbContext.PerformanceReviewHistories;
    public IQueryable<SelfAssessment> SelfAssessments => _dbContext.SelfAssessments;
    public IQueryable<PerformanceReviewCycle> PerformanceReviewCycles => _dbContext.PerformanceReviewCycles;
    public IQueryable<PerformanceGoal> PerformanceGoals => _dbContext.PerformanceGoals;
    public IQueryable<ImprovementPlan> ImprovementPlans => _dbContext.ImprovementPlans;
    public IQueryable<PeerFeedback> PeerFeedbacks => _dbContext.PeerFeedbacks;

    public IQueryable<CmScheme.Performance.Core.Views.AttendanceView> AttendanceViews => _dbContext.AttendanceViews;
    public IQueryable<CmScheme.Performance.Core.Views.LeaveApplicationView> LeaveApplicationViews => _dbContext.LeaveApplicationViews;
    public IQueryable<CmScheme.Performance.Core.Views.TaskProgressView> TaskProgressViews => _dbContext.TaskProgressViews;
    public IQueryable<CmScheme.Performance.Core.Views.WorkAllocationView> WorkAllocationViews => _dbContext.WorkAllocationViews;
}
