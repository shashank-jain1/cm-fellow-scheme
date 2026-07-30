using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.ModuleMaster.ListModules;

public sealed record ListModulesQuery : IQuery<Result<List<ListModulesResult>>>;

public sealed record ListModulesResult
{
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public string? Description { get; init; }
    public int SortOrder { get; init; }
}

public sealed class ListModulesQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<ListModulesQuery, Result<List<ListModulesResult>>>
{
    public async ValueTask<Result<List<ListModulesResult>>> Handle(
        ListModulesQuery request,
        CancellationToken cancellationToken)
    {
        List<ListModulesResult> modules = await dbContext.ModuleMasters
            .Where(mm => mm.IsActive)
            .OrderBy(mm => mm.SortOrder)
            .Select(mm => new ListModulesResult
            {
                ModuleMasterId = mm.ModuleMasterId,
                ModuleCode = mm.ModuleCode,
                ModuleName = mm.ModuleName,
                Description = mm.Description,
                SortOrder = mm.SortOrder,
            })
            .ToListAsync(cancellationToken);

        return Result<List<ListModulesResult>>.Success(modules);
    }
}
