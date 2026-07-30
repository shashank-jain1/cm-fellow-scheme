using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

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

public sealed class GetAuditLogQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetAuditLogQuery, Result<List<AuditLogDto>>>
{
    public async ValueTask<Result<List<AuditLogDto>>> Handle(
        GetAuditLogQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.ModuleAccessAuditLog> query = dbContext.ModuleAccessAuditLogs
            .AsNoTracking();

        if (request.UserAccountId.HasValue)
        {
            query = query.Where(a => a.UserAccountId == request.UserAccountId.Value);
        }

        if (request.ModuleMasterId.HasValue)
        {
            query = query.Where(a => a.ModuleMasterId == request.ModuleMasterId.Value);
        }

        List<Core.Entities.ModuleAccessAuditLog> logs = await query
            .OrderByDescending(a => a.PerformedOn)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        List<int> userIds = logs.SelectMany(a => new[] { a.UserAccountId, a.PerformedBy }).Distinct().ToList();
        List<int> moduleIds = logs.Select(a => a.ModuleMasterId).Distinct().ToList();

        List<Core.Entities.UserAccount> users = await dbContext.UserAccounts
            .Where(ua => userIds.Contains(ua.UserAccountId))
            .Include(ua => ua.Applicant)
            .ToListAsync(cancellationToken);

        List<Core.Entities.ModuleMaster> modules = await dbContext.ModuleMasters
            .Where(mm => moduleIds.Contains(mm.ModuleMasterId))
            .ToListAsync(cancellationToken);

        Dictionary<int, Core.Entities.UserAccount> userMap = users.ToDictionary(u => u.UserAccountId);
        Dictionary<int, Core.Entities.ModuleMaster> moduleMap = modules.ToDictionary(m => m.ModuleMasterId);

        List<AuditLogDto> result = logs.Select(a =>
        {
            string fullName = string.Empty;
            if (userMap.TryGetValue(a.UserAccountId, out Core.Entities.UserAccount? ua) && ua.Applicant != null)
            {
                fullName = $"{ua.Applicant.FirstName} {ua.Applicant.LastName}".Trim();
            }

            string moduleName = string.Empty;
            string moduleCode = string.Empty;
            if (moduleMap.TryGetValue(a.ModuleMasterId, out Core.Entities.ModuleMaster? mm))
            {
                moduleName = mm.ModuleName;
                moduleCode = mm.ModuleCode;
            }

            string performerName = string.Empty;
            if (userMap.TryGetValue(a.PerformedBy, out Core.Entities.UserAccount? performer))
            {
                performerName = performer.Username;
            }

            return new AuditLogDto
            {
                ModuleAccessAuditLogId = a.ModuleAccessAuditLogId,
                UserModuleAccessId = a.UserModuleAccessId,
                UserAccountId = a.UserAccountId,
                Username = userMap.TryGetValue(a.UserAccountId, out Core.Entities.UserAccount? u) ? u.Username : string.Empty,
                FullName = fullName,
                ModuleMasterId = a.ModuleMasterId,
                ModuleCode = moduleCode,
                ModuleName = moduleName,
                Action = a.Action,
                OldValues = a.OldValues,
                NewValues = a.NewValues,
                PerformedBy = a.PerformedBy,
                PerformerName = performerName,
                PerformedOn = a.PerformedOn,
                Reason = a.Reason,
            };
        }).ToList();

        return Result<List<AuditLogDto>>.Success(result);
    }
}
