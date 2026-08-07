using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.SearchArticles;

public sealed record SearchArticlesQuery : IQuery<Result<List<SearchArticlesResult>>>
{
    public string? SearchTerm { get; init; }
    public string? Category { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}
