using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CmScheme.Common.Core.Services;
using CmScheme.Common.Infrastructure.Services;
using CmScheme.Masters.Endpoints;
using CmScheme.Masters.Infrastructure;
using CmScheme.Masters.Infrastructure.Data;
using CmScheme.Registration.Endpoints;
using CmScheme.Registration.Infrastructure;
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
using CmScheme.Dashboard.Infrastructure;
using CmScheme.Administration.Endpoints;

namespace CmScheme.Api;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IConfiguration config)
    {
        string? jwtKey = config["Jwt:Key"];
        string? jwtIssuer = config["Jwt:Issuer"];
        string? jwtAudience = config["Jwt:Audience"];

        services.AddAuthentication(options =>
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

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("FellowPolicy", policy => policy.RequireRole("Admin", "Fellow"));
            options.AddPolicy("GuidePolicy", policy => policy.RequireRole("Admin", "Guide"));
            options.AddPolicy("ModuleAccess", policy => policy.RequireAuthenticatedUser());
        });

        return services;
    }

    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
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

        return services;
    }

    public static IServiceCollection AddAppRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
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

        return services;
    }

    public static IServiceCollection AddAppModules(this IServiceCollection services, string connectionString)
    {
        services
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

        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<FileStorageOptions>(config.GetSection("FileStorage"));
        services.AddHttpClient();
        services.AddScoped<IFileUploadService, LocalFileUploadService>();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<INotificationService, SmtpNotificationService>();
        services.AddScoped<IOtpService, InMemoryOtpService>();
        services.AddSingleton<IBusinessKeyGenerator, BusinessKeyGenerator>();
        services.AddScoped<IBulkImportService, BulkImportService>();
        services.AddScoped<IDatabaseBackupService, DatabaseBackupService>();
        services.AddScoped<ICertificateTemplateService, CertificateTemplateService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IGeoValidationService, GeoValidationService>();
        services.AddScoped<IFaceMatchService, FaceMatchService>();
        services.AddHostedService<BackgroundServices.SlaCheckBackgroundService>();

        return services;
    }
}
