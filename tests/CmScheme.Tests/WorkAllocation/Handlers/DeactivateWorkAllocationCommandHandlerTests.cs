using Ardalis.Result;
using CmScheme.Common.Core;
using CmScheme.WorkAllocation.Application.Features.WorkAllocation.DeactivateWorkAllocation;
using CmScheme.WorkAllocation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;

namespace CmScheme.Tests.WorkAllocation.Handlers;

public class DeactivateWorkAllocationCommandHandlerTests
{
    private static WorkAllocationDbContext BuildContext(string status, bool activeStatus = true)
    {
        return TestDbContext.CreateWorkAllocation(c =>
        {
            c.WorkAllocations.Add(new WorkAllocationEntity
            {
                ProjectId = 1,
                WorkProjectId = "WP-2026-001",
                WorkDescription = "Field survey",
                Priority = "High",
                StartDate = new DateTime(2026, 7, 1),
                EndDate = new DateTime(2026, 7, 10),
                DurationDays = 10,
                SurveysPerIntern = 25,
                ActiveStatus = activeStatus,
                Status = status,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "admin",
            });
            c.SaveChanges();
        });
    }

    /// <summary>
    /// Guards the defect where the handler compared against the literal "completed" while
    /// statuses are persisted as "Completed", so no allocation could ever be deactivated.
    /// </summary>
    [Fact]
    public async Task Handle_Completed_Allocation_Is_Deactivated()
    {
        var ctx = BuildContext(Statuses.WorkStatus.Completed);
        var handler = new DeactivateWorkAllocationCommandHandler(ctx);
        int id = await ctx.WorkAllocations.Select(w => w.WorkAllocationId).FirstAsync();

        Result result = await handler.Handle(
            new DeactivateWorkAllocationCommand { WorkAllocationId = id }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.False(await ctx.WorkAllocations.Select(w => w.ActiveStatus).FirstAsync());
    }

    [Fact]
    public async Task Handle_In_Progress_Allocation_Is_Rejected()
    {
        var ctx = BuildContext(Statuses.WorkStatus.InProgress);
        var handler = new DeactivateWorkAllocationCommandHandler(ctx);
        int id = await ctx.WorkAllocations.Select(w => w.WorkAllocationId).FirstAsync();

        Result result = await handler.Handle(
            new DeactivateWorkAllocationCommand { WorkAllocationId = id }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.True(await ctx.WorkAllocations.Select(w => w.ActiveStatus).FirstAsync());
    }

    [Fact]
    public async Task Handle_Already_Inactive_Allocation_Is_Rejected()
    {
        var ctx = BuildContext(Statuses.WorkStatus.Completed, activeStatus: false);
        var handler = new DeactivateWorkAllocationCommandHandler(ctx);
        int id = await ctx.WorkAllocations.Select(w => w.WorkAllocationId).FirstAsync();

        Result result = await handler.Handle(
            new DeactivateWorkAllocationCommand { WorkAllocationId = id }, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Unknown_Allocation_Returns_NotFound()
    {
        var ctx = BuildContext(Statuses.WorkStatus.Completed);
        var handler = new DeactivateWorkAllocationCommandHandler(ctx);

        Result result = await handler.Handle(
            new DeactivateWorkAllocationCommand { WorkAllocationId = 9999 }, CancellationToken.None);

        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
