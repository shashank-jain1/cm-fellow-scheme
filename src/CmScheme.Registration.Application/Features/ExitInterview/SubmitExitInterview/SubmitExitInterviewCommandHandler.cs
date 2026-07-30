using Ardalis.Result;
using CmScheme.Registration.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Registration.Application.Features.ExitInterview.SubmitExitInterview;

public sealed class SubmitExitInterviewCommandHandler(
    IRegistrationCommandDbContext dbContext)
    : ICommandHandler<SubmitExitInterviewCommand, Result>
{
    public async ValueTask<Result> Handle(
        SubmitExitInterviewCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.ExitInterview? existing = await dbContext.ExitInterviews
            .FirstOrDefaultAsync(e => e.UserAccountId == request.UserAccountId, cancellationToken);

        if (existing is not null)
        {
            existing.OverallExperience = request.OverallExperience;
            existing.WorkEnvironment = request.WorkEnvironment;
            existing.LearningOpportunities = request.LearningOpportunities;
            existing.TeamCollaboration = request.TeamCollaboration;
            existing.ImprovementSuggestions = request.ImprovementSuggestions;
            existing.WhatWorkedWell = request.WhatWorkedWell;
            existing.WouldRecommend = request.WouldRecommend;
            existing.SubmittedOn = DateTime.UtcNow;
        }
        else
        {
            Core.Entities.ExitInterview interview = new()
            {
                UserAccountId = request.UserAccountId,
                OverallExperience = request.OverallExperience,
                WorkEnvironment = request.WorkEnvironment,
                LearningOpportunities = request.LearningOpportunities,
                TeamCollaboration = request.TeamCollaboration,
                ImprovementSuggestions = request.ImprovementSuggestions,
                WhatWorkedWell = request.WhatWorkedWell,
                WouldRecommend = request.WouldRecommend,
                SubmittedOn = DateTime.UtcNow
            };
            dbContext.ExitInterviews.Add(interview);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.NoContent();
    }
}
