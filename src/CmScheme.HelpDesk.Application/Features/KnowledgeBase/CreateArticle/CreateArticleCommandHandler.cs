using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.CreateArticle;

public sealed class CreateArticleCommandHandler(
    IHelpDeskCommandDbContext helpDeskDbContext)
    : ICommandHandler<CreateArticleCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        KnowledgeBaseArticle article = new KnowledgeBaseArticle
        {
            Title = request.Title,
            Content = request.Content,
            Category = request.Category,
            Tags = request.Tags,
            AuthorId = request.AuthorId,
            AuthorName = request.AuthorName,
            ViewCount = 0,
            IsPublished = request.IsPublished,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        helpDeskDbContext.KnowledgeBaseArticles.Add(article);
        await helpDeskDbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(article.KnowledgeBaseArticleId);
    }
}
