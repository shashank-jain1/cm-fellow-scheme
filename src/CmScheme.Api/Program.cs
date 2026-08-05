using Serilog;
using Microsoft.AspNetCore.Authorization;
using CmScheme.Api;
using CmScheme.Api.SeedData;
using CmScheme.Common.Core.Data;
using CmScheme.Common.Core.Services;
using CmScheme.Common.Infrastructure.Services;
using CmScheme.Api.Authorization;
using CmScheme.Api.Middleware;
using CmScheme.Api.Services;
using CmScheme.Endpoints.Abstractions;

try
{
    Log.Information("Starting CmScheme API host");

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/cmscheme-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
        .CreateLogger();

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=localhost;Database=CmSchemeDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

    builder.Services
        .AddAppAuth(builder.Configuration)
        .AddAppSwagger()
        .AddAppRateLimiting()
        .AddAppModules(connectionString)
        .AddAppServices(builder.Configuration);

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
    builder.Services.AddScoped<IAuthorizationHandler, ModuleAuthorizationHandler>();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddMemoryCache();
    builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });
    builder.Services.AddOutputCache();
    builder.Services.AddHealthChecks().AddSqlServer(connectionString, name: "sqlserver");

    WebApplication app = builder.Build();

    app.UseExceptionHandler();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        await StartupMigrations.ApplyAsync(app);
    }

    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRateLimiter();
    app.UseMiddleware<AuditMiddleware>();
    app.UseStaticFiles();
    app.UseOutputCache();

    app.MapApiEndpoints("/api/v1");
    app.MapAppEndpoints();

    await StartupSeedData.RunAllAsync(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "CmScheme API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
