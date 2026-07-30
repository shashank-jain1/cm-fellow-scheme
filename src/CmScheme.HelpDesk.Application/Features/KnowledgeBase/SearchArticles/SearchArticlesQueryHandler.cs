using Ardalis.Result;
using CmScheme.HelpDesk.Core.Data;
using CmScheme.HelpDesk.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.HelpDesk.Application.Features.KnowledgeBase.SearchArticles;

public sealed class SearchArticlesQueryHandler(
    IHelpDeskQueryDbContext helpDeskDbContext)
    : IQueryHandler<SearchArticlesQuery, Result<List<SearchArticlesResult>>>
{
    public async ValueTask<Result<List<SearchArticlesResult>>> Handle(SearchArticlesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<KnowledgeBaseArticle> query = helpDeskDbContext.KnowledgeBaseArticles
            .Where(a => a.IsActive && a.IsPublished);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTerm = request.SearchTerm.ToLower();
            query = query.Where(a =>
                a.Title.ToLower().Contains(searchTerm) ||
                a.Content.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            query = query.Where(a => a.Category == request.Category);
        }

        List<SearchArticlesResult> results = await query
            .OrderByDescending(a => a.CreatedOn)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new SearchArticlesResult
            {
                KnowledgeBaseArticleId = a.KnowledgeBaseArticleId,
                Title = a.Title,
                Category = a.Category,
                AuthorName = a.AuthorName,
                ViewCount = a.ViewCount,
                CreatedOn = a.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result<List<SearchArticlesResult>>.Success(results);
    }
}
