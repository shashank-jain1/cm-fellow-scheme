namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.SearchArticles;

public sealed record SearchArticlesResult
{
    public int KnowledgeBaseArticleId { get; init; }
    public string Title { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string AuthorName { get; init; } = null!;
    public int ViewCount { get; init; }
    public DateTime CreatedOn { get; init; }
}
