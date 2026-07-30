using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Application.Features.UserModuleAccess.Dtos;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.ListModules;

public sealed class ListModulesQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<ListModulesQuery, Result<List<ModuleMasterDto>>>
{
    public async ValueTask<Result<List<ModuleMasterDto>>> Handle(
        ListModulesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.ModuleMaster> query = dbContext.ModuleMasters
            .AsNoTracking();

        if (request.IsActive.HasValue)
        {
            query = query.Where(mm => mm.IsActive == request.IsActive.Value);
        }

        List<ModuleMasterDto> modules = await query
            .OrderBy(mm => mm.SortOrder)
            .ThenBy(mm => mm.ModuleName)
            .Select(mm => new ModuleMasterDto
            {
                ModuleMasterId = mm.ModuleMasterId,
                ModuleCode = mm.ModuleCode,
                ModuleName = mm.ModuleName,
                Description = mm.Description,
                SortOrder = mm.SortOrder,
                IsActive = mm.IsActive
            })
            .ToListAsync(cancellationToken);

        return Result<List<ModuleMasterDto>>.Success(modules);
    }
}
