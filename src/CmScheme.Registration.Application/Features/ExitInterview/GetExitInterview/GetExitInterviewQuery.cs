using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.ExitInterview.GetExitInterview;

public sealed record GetExitInterviewQuery : IQuery<Result<ExitInterviewDto>>
{
    public int UserAccountId { get; init; }
}

public sealed record ExitInterviewDto
{
    public int ExitInterviewId { get; init; }
    public int UserAccountId { get; init; }
    public int OverallExperience { get; init; }
    public int WorkEnvironment { get; init; }
    public int LearningOpportunities { get; init; }
    public int TeamCollaboration { get; init; }
    public string? ImprovementSuggestions { get; init; }
    public string? WhatWorkedWell { get; init; }
    public bool WouldRecommend { get; init; }
    public DateTime SubmittedOn { get; init; }
}
