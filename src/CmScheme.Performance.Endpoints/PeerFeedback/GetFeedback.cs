using Ardalis.Result;
using CmScheme.Performance.Application.Features.Performance.PeerFeedback.GetFeedback;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.PeerFeedback;

public static class GetFeedback
{
    public static async Task<IResult> Handle(int performanceEvaluationId, ISender sender)
    {
        GetPeerFeedbackQuery query = new GetPeerFeedbackQuery
        {
            PerformanceEvaluationId = performanceEvaluationId
        };
        Result<List<PeerFeedbackResult>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
