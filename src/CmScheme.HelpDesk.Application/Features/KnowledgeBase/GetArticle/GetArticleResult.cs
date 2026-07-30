namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.GetArticle;

public sealed record GetArticleResult
{
    public int KnowledgeBaseArticleId { get; init; }
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string? Tags { get; init; }
    public string AuthorName { get; init; } = null!;
    public int ViewCount { get; init; }
    public bool IsPublished { get; init; }
    public DateTime CreatedOn { get; init; }
    public DateTime? ModifiedOn { get; init; }
}
