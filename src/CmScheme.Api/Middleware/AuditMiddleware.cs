using CmScheme.Common.Core.Services;

namespace CmScheme.Api.Middleware;

public sealed class AuditMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly HashSet<string> StaticExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".css", ".js", ".png", ".jpg", ".jpeg", ".gif", ".svg", ".ico",
        ".woff", ".woff2", ".ttf", ".eot", ".map"
    };

    public AuditMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (ShouldSkip(context))
        {
            await _next(context);
            return;
        }

        await _next(context);

        try
        {
            string method = context.Request.Method;
            string path = context.Request.Path.Value ?? "/";

            string action = MapMethodToAction(method);
            string entityName = ExtractEntityName(path);
            string? entityId = ExtractEntityId(path);

            int statusCode = context.Response.StatusCode;

            if (statusCode >= 400)
            {
                action = $"{action} (Failed: {statusCode})";
            }

            IAuditService auditService = context.RequestServices.GetRequiredService<IAuditService>();
            await auditService.LogAsync(action, entityName, entityId);
        }
        catch
        {
            // Swallow exceptions to prevent disrupting the response
        }
    }

    private static bool ShouldSkip(HttpContext context)
    {
        string method = context.Request.Method;
        string path = context.Request.Path.Value ?? string.Empty;

        if (HttpMethods.IsGet(method))
            return true;

        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
            return true;

        if (path.StartsWith("/api/v1/auth", StringComparison.OrdinalIgnoreCase))
            return true;

        string extension = Path.GetExtension(path);
        if (!string.IsNullOrEmpty(extension) && StaticExtensions.Contains(extension))
            return true;

        return false;
    }

    private static string MapMethodToAction(string method) => method.ToUpperInvariant() switch
    {
        "POST" => "Create",
        "PUT" => "Update",
        "PATCH" => "Update",
        "DELETE" => "Delete",
        _ => method
    };

    private static string ExtractEntityName(string path)
    {
        string[] segments = path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length == 0)
            return "System";

        if (segments.Length >= 2 && segments[0].Equals("api", StringComparison.OrdinalIgnoreCase))
        {
            return segments.Length >= 3 ? segments[2] : segments[1];
        }

        return segments[^1];
    }

    private static string? ExtractEntityId(string path)
    {
        string[] segments = path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length >= 4
            && segments[0].Equals("api", StringComparison.OrdinalIgnoreCase)
            && int.TryParse(segments[^1], out _))
        {
            return segments[^1];
        }

        return null;
    }
}
