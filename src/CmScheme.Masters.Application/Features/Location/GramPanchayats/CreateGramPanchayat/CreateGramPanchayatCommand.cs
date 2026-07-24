using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.CreateGramPanchayat;

public sealed record CreateGramPanchayatCommand(
    int BlockId,
    string GramPanchayatName,
    string? GPCode) : ICommand<Result<int>>;
