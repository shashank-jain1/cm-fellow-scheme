using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Performance.Application.Features.Performance.CalculatePerformanceScore;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Performance.Endpoints.Performance;

public sealed class CalculateScore
{
    public static async Task<IResult> Handle(int performanceEvaluationId, ISender sender, CancellationToken ct)
    {
        CalculatePerformanceScoreCommand command = new CalculatePerformanceScoreCommand
        {
            PerformanceEvaluationId = performanceEvaluationId
        };
        ValueTask<Result> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}
