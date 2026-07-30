using Ardalis.Result;
using CmScheme.HelpDesk.Application.Features.KnowledgeBase.CreateArticle;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.HelpDesk.Endpoints.KnowledgeBase;

public static class CreateArticle
{
    public static async Task<IResult> Handle(CreateArticleCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
