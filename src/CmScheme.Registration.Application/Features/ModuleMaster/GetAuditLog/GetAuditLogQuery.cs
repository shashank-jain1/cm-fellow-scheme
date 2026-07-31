using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.ModuleMaster.GetAuditLog;

public sealed record GetAuditLogQuery : IQuery<Result<List<AuditLogDto>>>
{
    public int? UserAccountId { get; init; }
    public int? ModuleMasterId { get; init; }
    public int PageSize { get; init; } = 50;
    public int PageNumber { get; init; } = 1;
}

public sealed record AuditLogDto
{
    public int ModuleAccessAuditLogId { get; init; }
    public int UserModuleAccessId { get; init; }
    public int UserAccountId { get; init; }
    public string Username { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public int ModuleMasterId { get; init; }
    public string ModuleCode { get; init; } = null!;
    public string ModuleName { get; init; } = null!;
    public string Action { get; init; } = null!;
    public string? OldValues { get; init; }
    public string? NewValues { get; init; }
    public int PerformedBy { get; init; }
    public string PerformerName { get; init; } = null!;
    public DateTime PerformedOn { get; init; }
    public string? Reason { get; init; }
}
