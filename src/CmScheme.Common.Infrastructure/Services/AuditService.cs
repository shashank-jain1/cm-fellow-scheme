using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class AuditService : IAuditService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(
        ICurrentUserService currentUserService,
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor)
    {
        _currentUserService = currentUserService;
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(
        string action,
        string entityName,
        string? entityId = null,
        string? oldValues = null,
        string? newValues = null,
        CancellationToken ct = default)
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;

        AuditLog auditLog = new AuditLog
        {
            UserId = _currentUserService.UserAccountId,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            OldValues = oldValues,
            NewValues = newValues,
            IpAddress = httpContext?.Connection?.RemoteIpAddress?.ToString(),
            UserAgent = httpContext?.Request?.Headers.UserAgent.ToString(),
            CreatedOn = DateTime.UtcNow,
        };

        using IServiceScope scope = _serviceProvider.CreateScope();
        IRegistrationCommandDbContext dbContext = scope.ServiceProvider
            .GetRequiredService<IRegistrationCommandDbContext>();

        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync(ct);
    }
}
