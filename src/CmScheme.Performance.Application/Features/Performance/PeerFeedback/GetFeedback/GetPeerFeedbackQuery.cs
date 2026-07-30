using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PeerFeedback.GetFeedback;

public sealed record GetPeerFeedbackQuery : IQuery<Result<List<PeerFeedbackResult>>>
{
    public int PerformanceEvaluationId { get; init; }
    public int? RevieweeId { get; init; }
}
