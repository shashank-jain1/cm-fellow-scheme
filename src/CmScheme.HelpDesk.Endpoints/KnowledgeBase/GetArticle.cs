using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.GetArticle;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.KnowledgeBase;

public static class GetArticle
{
    public static async Task<IResult> Handle(int knowledgeBaseArticleId, ISender sender)
    {
        GetArticleQuery query = new GetArticleQuery
        {
            KnowledgeBaseArticleId = knowledgeBaseArticleId
        };
        Result<GetArticleResult> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
