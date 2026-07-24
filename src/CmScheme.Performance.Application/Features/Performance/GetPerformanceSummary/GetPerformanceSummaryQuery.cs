using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.GetPerformanceSummary;

public sealed record GetPerformanceSummaryQuery(int ApplicantId) : IQuery<Result<Core.Dtos.PerformanceSummaryDto?>>;
