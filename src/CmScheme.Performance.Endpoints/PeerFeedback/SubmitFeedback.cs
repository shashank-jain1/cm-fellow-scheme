using Ardalis.Result;
using CmScheme.Performance.Application.Features.Performance.PeerFeedback.SubmitFeedback;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.PeerFeedback;

public static class SubmitFeedback
{
    public static async Task<IResult> Handle(SubmitPeerFeedbackCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
