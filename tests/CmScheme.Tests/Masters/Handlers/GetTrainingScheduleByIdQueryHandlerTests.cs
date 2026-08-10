using Ardalis.Result;
using CmScheme.Masters.Application.Features.TrainingSchedules.GetTrainingScheduleById;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class GetTrainingScheduleByIdQueryHandlerTests
{
    private static GetTrainingScheduleByIdQueryHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_TrainingSchedule_Returns_Response()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.TrainingSchedules.Add(new TrainingSchedule
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
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var tsId = await ctx.TrainingSchedules.Select(t => t.TrainingScheduleId).FirstAsync();

        var result = await handler.Handle(new GetTrainingScheduleByIdQuery { TrainingScheduleId = tsId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(tsId, result.Value.TrainingScheduleId);
        Assert.Equal("2026", result.Value.CalendarYear);
        Assert.Equal(1, result.Value.ProjectId);
        Assert.Equal(2, result.Value.WorkId);
        Assert.Equal(3, result.Value.DivisionId);
        Assert.Equal(4, result.Value.DistrictId);
        Assert.Equal(5, result.Value.BlockId);
        Assert.Equal("District Hall", result.Value.VenueName);
        Assert.Equal("Orientation", result.Value.TrainingDescription);
    }

    [Fact]
    public async Task Handle_Missing_TrainingSchedule_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new GetTrainingScheduleByIdQuery { TrainingScheduleId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}