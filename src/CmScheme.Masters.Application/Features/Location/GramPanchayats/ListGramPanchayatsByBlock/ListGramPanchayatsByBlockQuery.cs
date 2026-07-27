using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.ListGramPanchayatsByBlock;

public sealed record ListGramPanchayatsByBlockQuery : IQuery<Result<List<Core.Dtos.GramPanchayatDto>>>
{
    public int BlockId { get; init; }
}
