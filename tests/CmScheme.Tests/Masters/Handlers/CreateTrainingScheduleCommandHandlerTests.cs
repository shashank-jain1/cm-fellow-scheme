using CmScheme.Masters.Application.Features.TrainingSchedules.CreateTrainingSchedule;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateTrainingScheduleCommandHandlerTests
{
    private static CreateTrainingScheduleCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_TrainingSchedule_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateTrainingScheduleCommand
        {
            CalendarYear = "2026",
            ProjectId = 1,
            WorkId = 2,
            DivisionId = 3,
            DistrictId = 4,
            BlockId = 5,
            TrainingDate = new DateTime(2026, 6, 1),
            VenueName = "District Hall",
            TrainingDescription = "Orientation"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.TrainingSchedules.CountAsync());
        var ts = await ctx.TrainingSchedules.FirstAsync();
        Assert.Equal("2026", ts.CalendarYear);
        Assert.Equal(1, ts.ProjectId);
        Assert.Equal(2, ts.WorkId);
        Assert.Equal(3, ts.DivisionId);
        Assert.Equal(4, ts.DistrictId);
        Assert.Equal(5, ts.BlockId);
        Assert.Equal("District Hall", ts.VenueName);
        Assert.True(ts.IsActive);
    }

    [Fact]
    public async Task Handle_Command_Persists_Nullable_Fields()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateTrainingScheduleCommand
        {
            CalendarYear = "2026",
            ProjectId = 1,
            TrainingDate = new DateTime(2026, 6, 1)
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        var ts = await ctx.TrainingSchedules.FirstAsync();
        Assert.Null(ts.WorkId);
        Assert.Null(ts.DivisionId);
        Assert.Null(ts.DistrictId);
        Assert.Null(ts.BlockId);
        Assert.Null(ts.VenueName);
    }
}