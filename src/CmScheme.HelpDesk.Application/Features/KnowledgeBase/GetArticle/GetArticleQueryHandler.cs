using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.GetArticle;

public sealed class GetArticleQueryHandler(
    IHelpDeskQueryDbContext helpDeskDbContext,
    IHelpDeskCommandDbContext commandDbContext)
    : IQueryHandler<GetArticleQuery, Result<GetArticleResult>>
{
    public async ValueTask<Result<GetArticleResult>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        KnowledgeBaseArticle? article = await helpDeskDbContext.KnowledgeBaseArticles
            .FirstOrDefaultAsync(a => a.KnowledgeBaseArticleId == request.KnowledgeBaseArticleId && a.IsActive, cancellationToken);

        if (article == null)
        {
            return Result<GetArticleResult>.NotFound();
        }

        article.ViewCount++;
        await commandDbContext.SaveChangesAsync(cancellationToken);

        GetArticleResult result = new GetArticleResult
        {
            KnowledgeBaseArticleId = article.KnowledgeBaseArticleId,
            Title = article.Title,
            Content = article.Content,
            Category = article.Category,
            Tags = article.Tags,
            AuthorName = article.AuthorName,
            ViewCount = article.ViewCount,
            IsPublished = article.IsPublished,
            CreatedOn = article.CreatedOn,
            ModifiedOn = article.ModifiedOn
        };

        return Result<GetArticleResult>.Success(result);
    }
}
