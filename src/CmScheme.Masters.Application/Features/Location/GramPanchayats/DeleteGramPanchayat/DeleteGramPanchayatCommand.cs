using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.DeleteGramPanchayat;

public sealed record DeleteGramPanchayatCommand : ICommand<Result>
{
    public int GramPanchayatId { get; init; }
}
