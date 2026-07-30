using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.GetArticle;

public sealed record GetArticleQuery : IQuery<Result<GetArticleResult>>
{
    public int KnowledgeBaseArticleId { get; init; }
}
