using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.CreateArticle;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.SearchArticles;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.GetArticle;

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
            .WithTags("Knowledge Base")
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapGet("/search", Search.Handle)
            .WithName("SearchKnowledgeBaseArticles")
            .WithDisplayName("Search knowledge base articles")
            .WithTags("Knowledge Base")
            .Produces<List<SearchArticlesResult>>();

        group.MapGet("/{knowledgeBaseArticleId:int}", GetArticle.Handle)
            .WithName("GetKnowledgeBaseArticle")
            .WithDisplayName("Get knowledge base article")
            .WithTags("Knowledge Base")
            .Produces<GetArticleResult>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return builder;
    }
}
