using Ardalis.Result;
using Mediator;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.BulkUpdateModuleAccess;

public sealed record BulkUpdateModuleAccessCommand : ICommand<Result<int>>
{
    public int UserAccountId { get; init; }
    public List<ModuleAccessItemDto> Accesses { get; init; } = [];
    public int PerformedBy { get; init; }
}
