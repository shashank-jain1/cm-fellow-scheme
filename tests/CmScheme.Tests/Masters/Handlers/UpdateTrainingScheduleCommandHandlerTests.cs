using Ardalis.Result;
using CmScheme.Masters.Application.Features.TrainingSchedules.UpdateTrainingSchedule;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateTrainingScheduleCommandHandlerTests
{
    private static UpdateTrainingScheduleCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_TrainingSchedule_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.TrainingSchedules.Add(new TrainingSchedule
            {
                CalendarYear = "2026",
                ProjectId = 1,
                TrainingDate = new DateTime(2026, 6, 1),
                VenueName = "Old Venue"
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var tsId = await ctx.TrainingSchedules.Select(t => t.TrainingScheduleId).FirstAsync();

        var result = await handler.Handle(new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = tsId,
            CalendarYear = "2027",
            ProjectId = 2,
            WorkId = 3,
            DivisionId = 4,
            DistrictId = 5,
            BlockId = 6,
            TrainingDate = new DateTime(2027, 7, 1),
            VenueName = "New Venue",
            TrainingDescription = "Refresher"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.TrainingSchedules.FirstAsync(t => t.TrainingScheduleId == tsId);
        Assert.Equal("2027", updated.CalendarYear);
        Assert.Equal(2, updated.ProjectId);
        Assert.Equal(3, updated.WorkId);
        Assert.Equal("New Venue", updated.VenueName);
        Assert.Equal("Refresher", updated.TrainingDescription);
    }

    [Fact]
    public async Task Handle_Missing_TrainingSchedule_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateTrainingScheduleCommand
        {
            TrainingScheduleId = 999,
            CalendarYear = "2026",
            ProjectId = 1,
            TrainingDate = new DateTime(2026, 6, 1)
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}