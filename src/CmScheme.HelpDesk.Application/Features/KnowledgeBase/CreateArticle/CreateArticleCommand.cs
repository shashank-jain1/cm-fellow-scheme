using Ardalis.Result;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.CreateArticle;

public sealed record CreateArticleCommand : ICommand<Result<int>>
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string? Tags { get; init; }
    public int AuthorId { get; init; }
    public string AuthorName { get; init; } = null!;
    public bool IsPublished { get; init; }
}
