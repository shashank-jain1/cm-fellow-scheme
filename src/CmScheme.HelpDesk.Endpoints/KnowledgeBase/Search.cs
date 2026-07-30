using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.SearchArticles;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.KnowledgeBase;

public static class Search
{
    public static async Task<IResult> Handle([AsParameters] SearchArticlesQuery query, ISender sender)
    {
        Result<List<SearchArticlesResult>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
