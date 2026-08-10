using Ardalis.Result;
using CmScheme.Masters.Application.Features.Works.UpdateWork;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateWorkCommandHandlerTests
{
    private static UpdateWorkCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Work_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Works.Add(new Work
            {
                ProjectId = 1,
                WorkName = "Old",
                Priority = "Low",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                AssignedTo = "A"
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var workId = await ctx.Works.Select(w => w.WorkId).FirstAsync();

        var result = await handler.Handle(new UpdateWorkCommand
        {
            WorkId = workId,
            ProjectId = 2,
            WorkName = "New",
            WorkDescription = "Desc",
            Priority = "High",
            StartDate = new DateTime(2026, 2, 1),
            EndDate = new DateTime(2026, 4, 1),
            AssignedTo = "B",
            Remarks = "Updated"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.Works.FirstAsync(w => w.WorkId == workId);
        Assert.Equal(2, updated.ProjectId);
        Assert.Equal("New", updated.WorkName);
        Assert.Equal("High", updated.Priority);
        Assert.Equal("B", updated.AssignedTo);
    }

    [Fact]
    public async Task Handle_Missing_Work_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateWorkCommand
        {
            WorkId = 999,
            ProjectId = 1,
            WorkName = "X",
            Priority = "High",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow,
            AssignedTo = "A"
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}