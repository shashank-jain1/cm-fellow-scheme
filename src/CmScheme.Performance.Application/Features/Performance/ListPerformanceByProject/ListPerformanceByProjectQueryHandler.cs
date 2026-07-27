using Ardalis.Result;
using CmScheme.Performance.Core.Data;
using CmScheme.Performance.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Performance.Application.Features.Performance.ListPerformanceByProject;

public sealed class ListPerformanceByProjectQueryHandler(IPerformanceQueryDbContext dbContext)
    : IQueryHandler<ListPerformanceByProjectQuery, Result<List<PerformanceListItemDto>>>
{
    public async ValueTask<Result<List<PerformanceListItemDto>>> Handle(ListPerformanceByProjectQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.PerformanceEvaluation> query = dbContext.PerformanceEvaluations;

        if (!string.IsNullOrEmpty(request.ProjectName))
        {
            query = query.Where(p => p.ProjectName == request.ProjectName);
        }

        List<PerformanceListItemDto> items = await query
            .Select(p => new PerformanceListItemDto
            {
                PerformanceEvaluationId = p.PerformanceEvaluationId,
                ProjectName = p.ProjectName,
                ApplicantNumber = p.ApplicantNumber,
                ApplicantName = p.ApplicantName,
                PerformanceScore = p.PerformanceScore,
                PerformanceGrade = p.PerformanceGrade,
                CompletionPercentage = p.CompletionPercentage,
                PerformanceStatus = p.PerformanceStatus
            })
            .ToListAsync(cancellationToken);

        return Result<List<PerformanceListItemDto>>.Success(items);
    }
}
