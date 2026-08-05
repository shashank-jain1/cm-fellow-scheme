using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.UpdateGramPanchayat;

public sealed record UpdateGramPanchayatCommand : ICommand<Result>
{
    public int GramPanchayatId { get; init; }
    public int BlockId { get; init; }
    public string GramPanchayatName { get; init; } = null!;
    public string? GPCode { get; init; }
}
