using Ardalis.Result;
using CmScheme.Masters.Application.Features.Works.GetWorkById;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class GetWorkByIdQueryHandlerTests
{
    private static GetWorkByIdQueryHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Work_Returns_Mapped_Command()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Works.Add(new Work
            {
                ProjectId = 1,
                WorkName = "Survey works",
                WorkDescription = "Field survey",
                Priority = "High",
                StartDate = new DateTime(2026, 2, 1),
                EndDate = new DateTime(2026, 4, 1),
                AssignedTo = "Engineer",
                Remarks = "First phase"
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var workId = await ctx.Works.Select(w => w.WorkId).FirstAsync();

        var result = await handler.Handle(new GetWorkByIdQuery { WorkId = workId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value.ProjectId);
        Assert.Equal("Survey works", result.Value.WorkName);
        Assert.Equal("Field survey", result.Value.WorkDescription);
        Assert.Equal("High", result.Value.Priority);
        Assert.Equal("Engineer", result.Value.AssignedTo);
        Assert.Equal("First phase", result.Value.Remarks);
    }

    [Fact]
    public async Task Handle_Missing_Work_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new GetWorkByIdQuery { WorkId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}