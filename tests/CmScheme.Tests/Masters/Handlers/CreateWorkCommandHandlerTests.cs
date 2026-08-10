using CmScheme.Masters.Application.Features.Works.CreateWork;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateWorkCommandHandlerTests
{
    private static CreateWorkCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_Work_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateWorkCommand
        {
            ProjectId = 1,
            WorkName = "Survey works",
            WorkDescription = "Field survey",
            Priority = "High",
            StartDate = new DateTime(2026, 2, 1),
            EndDate = new DateTime(2026, 4, 1),
            AssignedTo = "Engineer",
            Remarks = "First phase"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.Works.CountAsync());
        var work = await ctx.Works.FirstAsync();
        Assert.Equal(1, work.ProjectId);
        Assert.Equal("Survey works", work.WorkName);
        Assert.Equal("High", work.Priority);
        Assert.Equal("Engineer", work.AssignedTo);
        Assert.True(work.IsActive);
    }
}