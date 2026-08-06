using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.GetArticle;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.SearchArticles;

namespace CmScheme.HelpDesk.Endpoints.KnowledgeBase;

public static class KnowledgeBaseGroupExtensions
{
    public static IEndpointRouteBuilder MapKnowledgeBaseEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("knowledge-base")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.HelpDesk, "Read", requireScope: false);

        group.MapPost("/", CreateArticle.Handle)
            .WithName("CreateKnowledgeBaseArticle")
            .WithDisplayName("Create knowledge base article")
            .DisableAntiforgery()
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/search", Search.Handle)
            .WithName("SearchKnowledgeBaseArticles")
            .WithDisplayName("Search knowledge base articles")
            .Produces<List<SearchArticlesResult>>();

        group.MapGet("/{knowledgeBaseArticleId:int}", GetArticle.Handle)
            .WithName("GetKnowledgeBaseArticleById")
            .WithDisplayName("Get knowledge base article by ID")
            .Produces<GetArticleResult>();

        return group;
    }
}
