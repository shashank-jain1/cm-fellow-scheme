using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CmScheme.Api;

internal static class AppEndpointExtensions
{
    public static WebApplication MapAppEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                Dictionary<string, object> result = new Dictionary<string, object>
                {
                    ["status"] = report.Status.ToString(),
                    ["checks"] = report.Entries.Select(entry => new Dictionary<string, object>
                    {
                        ["name"] = entry.Key,
                        ["status"] = entry.Value.Status.ToString(),
                        ["duration"] = entry.Value.Duration.ToString(),
                        ["description"] = entry.Value.Description ?? "",
                        ["exception"] = entry.Value.Exception?.Message ?? "",
                    }).ToArray(),
                };
                await context.Response.WriteAsJsonAsync(result);
            },
        });

        return app;
    }
}
