using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.ExitInterview.SubmitExitInterview;

public sealed record SubmitExitInterviewCommand : ICommand<Result>
{
    public int UserAccountId { get; init; }
    public int OverallExperience { get; init; }
    public int WorkEnvironment { get; init; }
    public int LearningOpportunities { get; init; }
    public int TeamCollaboration { get; init; }
    public string? ImprovementSuggestions { get; init; }
    public string? WhatWorkedWell { get; init; }
    public bool WouldRecommend { get; init; }
}
