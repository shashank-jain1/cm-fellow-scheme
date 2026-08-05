using System.Text;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.RateLimiting;
using Serilog;
using CmScheme.Common.Core.Data;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Masters.Infrastructure;
using CmScheme.Masters.Infrastructure.Data;
using CmScheme.Masters.Endpoints;
using CmScheme.Api.SeedData;
using CmScheme.Registration.Infrastructure;
using CmScheme.Registration.Endpoints;
using CmScheme.Training.Endpoints;
using CmScheme.Training.Infrastructure;
using CmScheme.Training.Infrastructure.Data;
using CmScheme.WorkAllocation.Endpoints;
using CmScheme.WorkAllocation.Infrastructure;
using CmScheme.AttendanceLeave.Endpoints;
using CmScheme.AttendanceLeave.Infrastructure;
using CmScheme.Performance.Endpoints;
using CmScheme.Performance.Infrastructure;
using CmScheme.Certificate.Endpoints;
using CmScheme.Certificate.Infrastructure;
using CmScheme.HelpDesk.Endpoints;
using CmScheme.HelpDesk.Infrastructure;
using CmScheme.Dashboard.Endpoints;
using CmScheme.Administration.Endpoints;
using CmScheme.Dashboard.Infrastructure;
using CmScheme.Common.Core.Services;
using CmScheme.Common.Infrastructure.Services;
using CmScheme.Api.Authorization;
using CmScheme.Api.Middleware;
using CmScheme.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

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

string? jwtKey = builder.Configuration["Jwt:Key"];
string? jwtIssuer = builder.Configuration["Jwt:Issuer"];
string? jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey ?? "CmScheme@2025!SecretKey#ForJwtTokenGeneration$VeryLong32Chars+")),
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("FellowPolicy", policy => policy.RequireRole("Admin", "Fellow"));
    options.AddPolicy("GuidePolicy", policy => policy.RequireRole("Admin", "Guide"));
    options.AddPolicy("ModuleAccess", policy =>
        policy.RequireAuthenticatedUser());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAuthorizationHandler, ModuleAuthorizationHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddMemoryCache();

builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CM Fellow API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddOutputCache();

builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, name: "sqlserver");

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: "global",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
            }));

    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.ContentType = "application/problem+json";

        int retryAfter = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out TimeSpan window)
            ? (int)window.TotalSeconds
            : 60;

        Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status429TooManyRequests,
            Title = "Rate limit exceeded",
            Detail = $"Too many requests. Retry after {retryAfter} seconds.",
            Type = "https://httpstatuses.com/429",
        };
        problemDetails.Extensions["retryAfter"] = retryAfter;

        await context.HttpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    };
});

builder.Services
    .AddMastersApis().AddMastersServices(connectionString).AddMastersInfrastructure(connectionString)
    .AddRegistrationApis().AddRegistrationServices(connectionString).AddRegistrationInfrastructure(connectionString)
    .AddTrainingApis().AddTrainingServices(connectionString).AddTrainingInfrastructure(connectionString)
    .AddWorkAllocationApis().AddWorkAllocationServices(connectionString).AddWorkAllocationInfrastructure(connectionString)
    .AddAttendanceLeaveApis().AddAttendanceLeaveServices(connectionString).AddAttendanceLeaveInfrastructure(connectionString)
    .AddPerformanceApis().AddPerformanceServices(connectionString).AddPerformanceInfrastructure(connectionString)
    .AddCertificateApis().AddCertificateServices(connectionString).AddCertificateInfrastructure(connectionString)
    .AddHelpDeskApis().AddHelpDeskServices(connectionString).AddHelpDeskInfrastructure(connectionString)
    .AddDashboardApis().AddDashboardServices(connectionString).AddDashboardInfrastructure(connectionString)
    .AddAdministrationApis();

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorage"));
builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();

builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<INotificationService, SmtpNotificationService>();
builder.Services.AddScoped<IOtpService, InMemoryOtpService>();
builder.Services.AddSingleton<IBusinessKeyGenerator, BusinessKeyGenerator>();
builder.Services.AddScoped<IBulkImportService, BulkImportService>();
builder.Services.AddScoped<IDatabaseBackupService, DatabaseBackupService>();
builder.Services.AddScoped<ICertificateTemplateService, CertificateTemplateService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IGeoValidationService, GeoValidationService>();
builder.Services.AddHostedService<CmScheme.Api.BackgroundServices.SlaCheckBackgroundService>();

WebApplication app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using IServiceScope migrationScope = app.Services.CreateScope();
    IServiceProvider sp = migrationScope.ServiceProvider;

    (string Name, DbContext Context)[] contexts =
    [
        ("Masters", sp.GetRequiredService<MastersCommandDbContext>()),
        ("Registration", sp.GetRequiredService<RegistrationCommandDbContext>()),
        ("Training", sp.GetRequiredService<TrainingCommandDbContext>()),
        ("WorkAllocation", sp.GetRequiredService<WorkAllocationDbContext>()),
        ("AttendanceLeave", sp.GetRequiredService<AttendanceLeaveDbContext>()),
        ("Certificate", sp.GetRequiredService<CertificateDbContext>()),
        ("HelpDesk", sp.GetRequiredService<HelpDeskDbContext>()),
        ("Performance", sp.GetRequiredService<PerformanceDbContext>()),
        ("Dashboard", sp.GetRequiredService<DashboardDbContext>()),
    ];

    foreach ((string Name, DbContext Context) in contexts)
    {
        try
        {
            await Context.Database.MigrateAsync();
        }
        catch (Exception)
        {
            try
            {
                await Context.Database.EnsureCreatedAsync();
            }
            catch (Exception)
            {
                // Skip context if neither Migrate nor EnsureCreated works
            }
        }
    }

    try
    {
        MastersCommandDbContext mastersCtx = sp.GetRequiredService<MastersCommandDbContext>();
        await mastersCtx.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Departments' AND type = 'U')
            BEGIN
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Department' AND type = 'U')
                    EXEC sp_rename 'Department', 'Departments';
                ELSE
                    CREATE TABLE [Departments] (
                        [DepartmentId] INT NOT NULL IDENTITY(1,1),
                        [DepartmentName] NVARCHAR(150) NOT NULL,
                        [DepartmentCode] NVARCHAR(50) NULL,
                        [IsActive] BIT NOT NULL DEFAULT 1,
                        [CreatedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                        [CreatedBy] INT NULL,
                        [ModifiedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                        [ModifiedBy] INT NULL,
                        CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
                    );
            END
        ");
    }
    catch (Exception)
    {
        // Department table fix failed silently
    }
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.UseMiddleware<CmScheme.Api.Middleware.AuditMiddleware>();

app.UseStaticFiles();
app.UseOutputCache();

app.MapApiEndpoints("/api/v1");

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


