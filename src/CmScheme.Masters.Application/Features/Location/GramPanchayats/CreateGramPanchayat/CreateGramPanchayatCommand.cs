using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.CreateGramPanchayat;

public sealed record CreateGramPanchayatCommand : ICommand<Result<int>>
{
    public int BlockId { get; init; }
    public string GramPanchayatName { get; init; } = null!;
    public string? GPCode { get; init; }
}
