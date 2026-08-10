using CmScheme.Masters.Application.Features.Location.States.ListStates;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListStatesQueryHandlerTests
{
    private static ListStatesQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Returns_States_Ordered_By_DisplayOrder_Then_Name()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.States.AddRange(
                new State { StateName = "Kerala", StateCode = "KL", DisplayOrder = 2 },
                new State { StateName = "Bihar", StateCode = "BR", DisplayOrder = 1 },
                new State { StateName = "Andhra Pradesh", StateCode = "AP", DisplayOrder = 1 });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListStatesQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value.Count);
        Assert.Equal("Andhra Pradesh", result.Value[0].StateName);
        Assert.Equal("Bihar", result.Value[1].StateName);
        Assert.Equal("Kerala", result.Value[2].StateName);
        Assert.Equal("AP", result.Value[0].StateCode);
    }

    [Fact]
    public async Task Handle_Empty_Store_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListStatesQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}