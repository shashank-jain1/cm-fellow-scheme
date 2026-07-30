using Ardalis.Result;
using CmScheme.Registration.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Registration.Application.Features.ExitInterview.GetExitInterview;

public sealed class GetExitInterviewQueryHandler(
    IRegistrationQueryDbContext dbContext)
    : IQueryHandler<GetExitInterviewQuery, Result<ExitInterviewDto>>
{
    public async ValueTask<Result<ExitInterviewDto>> Handle(
        GetExitInterviewQuery request,
        CancellationToken cancellationToken)
    {
        ExitInterviewDto? interview = await dbContext.ExitInterviews
            .AsNoTracking()
            .Where(e => e.UserAccountId == request.UserAccountId)
            .OrderByDescending(e => e.SubmittedOn)
            .Select(e => new ExitInterviewDto
            {
                ExitInterviewId = e.ExitInterviewId,
                UserAccountId = e.UserAccountId,
                OverallExperience = e.OverallExperience,
                WorkEnvironment = e.WorkEnvironment,
                LearningOpportunities = e.LearningOpportunities,
                TeamCollaboration = e.TeamCollaboration,
                ImprovementSuggestions = e.ImprovementSuggestions,
                WhatWorkedWell = e.WhatWorkedWell,
                WouldRecommend = e.WouldRecommend,
                SubmittedOn = e.SubmittedOn
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (interview is null)
        {
            return Result.NotFound("Exit interview not found.");
        }

        return Result<ExitInterviewDto>.Success(interview);
    }
}
