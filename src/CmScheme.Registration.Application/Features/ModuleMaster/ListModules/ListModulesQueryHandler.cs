using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.ModuleMaster.ListModules;

public sealed class ListModulesQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<ListModulesQuery, Result<List<ListModulesResult>>>
{
    public async ValueTask<Result<List<ListModulesResult>>> Handle(
        ListModulesQuery request,
        CancellationToken cancellationToken)
    {
        List<ListModulesResult> allModules = await dbContext.ModuleMasters
            .Where(mm => mm.IsActive)
            .OrderBy(mm => mm.SortOrder)
            .Select(mm => new ListModulesResult
            {
                ModuleMasterId = mm.ModuleMasterId,
                ModuleCode = mm.ModuleCode,
                ModuleName = mm.ModuleName,
                Description = mm.Description,
                SortOrder = mm.SortOrder,
                ParentModuleMasterId = mm.ParentModuleMasterId,
            })
            .ToListAsync(cancellationToken);

        List<ListModulesResult> parents = allModules
            .Where(m => m.ParentModuleMasterId == null)
            .ToList();

        List<ListModulesResult> result = new();
        foreach (ListModulesResult parent in parents)
        {
            List<ListModulesResult> children = allModules
                .Where(m => m.ParentModuleMasterId == parent.ModuleMasterId)
                .ToList();
            result.Add(parent with { Children = children });
        }

        return Result<List<ListModulesResult>>.Success(result);
    }
}
