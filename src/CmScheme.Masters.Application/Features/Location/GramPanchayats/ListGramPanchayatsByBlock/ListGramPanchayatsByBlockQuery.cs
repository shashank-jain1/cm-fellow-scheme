using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.ListGramPanchayatsByBlock;

public sealed record ListGramPanchayatsByBlockQuery(int BlockId) : IQuery<Result<List<Core.Dtos.GramPanchayatDto>>>;
