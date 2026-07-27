using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.GetPerformanceSummary;

public sealed record GetPerformanceSummaryQuery : IQuery<Result<Core.Dtos.PerformanceSummaryDto?>>
{
    public int? ApplicantId { get; init; }
}
