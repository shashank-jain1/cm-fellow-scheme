using System.Security.Cryptography;
using System.Text;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.RateLimiting;
using Serilog;
using CmScheme.Common.Core.Behaviours;
using CmScheme.Common.Core.Data;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Endpoints;
using CmScheme.Masters.Infrastructure;
using CmScheme.Masters.Infrastructure.Data;
using CmScheme.Api.SeedData;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
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
            ELSE IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Department' AND type = 'U')
            BEGIN
                EXEC sp_rename 'Department', 'Departments';
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

await SeedAdminUser(app);
await SeedLookupMasters(app);
await SeedModuleMaster(app);
await SeedAdminModuleAccess(app);
await SeedAdminUserRole(app);
await SeedLeaveTypes(app);
await SeedTicketCategories(app);
await LocationSeedData.SeedAsync(app.Services.CreateScope().ServiceProvider.GetRequiredService<IMastersCommandDbContext>());

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

static async Task SeedAdminUser(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    bool adminExists = await dbContext.UserAccounts
        .AnyAsync(ua => ua.Username == "admin");

    if (adminExists)
    {
        return;
    }

    Applicant adminApplicant = new Applicant
    {
        FirstName = "Admin",
        LastName = "User",
        FatherName = "System",
        EmailId = "admin@cmfellow.gov.in",
        MobileNumber = "9999999999",
        DateOfBirth = new DateTime(1990, 1, 1),
        PermanentAddress = "System Admin, Bhopal",
        PinCode = "462001",
        BoardUniversityName = "N/A",
        PassingYear = 2020,
        PercentageCGPA = 0,
        Status = "Approved",
        CreatedOn = DateTime.UtcNow,
    };

    dbContext.Applicants.Add(adminApplicant);
    await dbContext.SaveChangesAsync();

    byte[] salt = RandomNumberGenerator.GetBytes(16);
    using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes("Admin@123", salt, 100000, HashAlgorithmName.SHA256);
    byte[] hash = pbkdf2.GetBytes(32);
    byte[] hashBytes = new byte[48];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 32);

    UserAccount adminAccount = new UserAccount
    {
        ApplicantId = adminApplicant.ApplicantId,
        Username = "admin",
        PasswordHash = Convert.ToBase64String(hashBytes),
        Role = "Admin",
        IsActive = true,
        CreatedOn = DateTime.UtcNow,
    };

    dbContext.UserAccounts.Add(adminAccount);
    await dbContext.SaveChangesAsync();
}

static async Task SeedLookupMasters(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IMastersCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IMastersCommandDbContext>();

    bool anyExists = await dbContext.LookupMasters.AnyAsync();
    if (anyExists)
    {
        return;
    }

    List<LookupMaster> masters =
    [
        // Leave Types
        new() { MasterType = "LeaveType", Label = "Casual Leave", Value = "casual", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "LeaveType", Label = "Sick Leave", Value = "sick", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "LeaveType", Label = "Earned Leave", Value = "earned", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "LeaveType", Label = "Maternity Leave", Value = "maternity", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "LeaveType", Label = "Paternity Leave", Value = "paternity", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "LeaveType", Label = "Unpaid Leave", Value = "unpaid", SortOrder = 6, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Training Categories
        new() { MasterType = "TrainingCategory", Label = "Technical", Value = "Technical", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TrainingCategory", Label = "Soft Skills", Value = "Soft Skills", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TrainingCategory", Label = "Domain", Value = "Domain", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TrainingCategory", Label = "Leadership", Value = "Leadership", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TrainingCategory", Label = "Other", Value = "Other", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Ticket Categories
        new() { MasterType = "TicketCategory", Label = "Technical Issue", Value = "Technical Issue", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TicketCategory", Label = "Account Access", Value = "Account Access", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TicketCategory", Label = "Survey Problem", Value = "Survey Problem", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TicketCategory", Label = "Attendance Issue", Value = "Attendance Issue", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TicketCategory", Label = "Other", Value = "Other", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Priorities
        new() { MasterType = "Priority", Label = "High", Value = "High", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Priority", Label = "Medium", Value = "Medium", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Priority", Label = "Low", Value = "Low", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Genders
        new() { MasterType = "Gender", Label = "Male", Value = "male", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Gender", Label = "Female", Value = "female", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Gender", Label = "Other", Value = "other", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Qualifications
        new() { MasterType = "Qualification", Label = "Bachelor's Degree", Value = "bachelors", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Qualification", Label = "Master's Degree", Value = "masters", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Qualification", Label = "PhD", Value = "phd", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Qualification", Label = "Diploma", Value = "diploma", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Activity Modes
        new() { MasterType = "ActivityMode", Label = "Online", Value = "Online", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "ActivityMode", Label = "Offline", Value = "Offline", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "ActivityMode", Label = "Hybrid", Value = "Hybrid", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Meeting Agendas
        new() { MasterType = "MeetingAgenda", Label = "Review", Value = "Review", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "MeetingAgenda", Label = "Planning", Value = "Planning", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "MeetingAgenda", Label = "Discussion", Value = "Discussion", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "MeetingAgenda", Label = "Decision", Value = "Decision", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "MeetingAgenda", Label = "Other", Value = "Other", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Roles
        new() { MasterType = "Role", Label = "Admin", Value = "Admin", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Role", Label = "Fellow", Value = "Fellow", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Role", Label = "Intern", Value = "Intern", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Role", Label = "Guide", Value = "Guide", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Training Types
        new() { MasterType = "TrainingType", Label = "Fellow", Value = "Fellow", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TrainingType", Label = "Coordinator", Value = "Coordinator", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "TrainingType", Label = "Intern", Value = "Intern", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Years
        new() { MasterType = "Year", Label = "2020", Value = "2020", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Year", Label = "2021", Value = "2021", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Year", Label = "2022", Value = "2022", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Year", Label = "2023", Value = "2023", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Year", Label = "2024", Value = "2024", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Year", Label = "2025", Value = "2025", SortOrder = 6, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Year", Label = "2026", Value = "2026", SortOrder = 7, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Venue (admin-configured at runtime; no seed entries)

        // Platforms
        new() { MasterType = "Platform", Label = "Zoom", Value = "Zoom", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Platform", Label = "Google Meet", Value = "Google Meet", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Platform", Label = "Microsoft Teams", Value = "Microsoft Teams", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Financial Years
        new() { MasterType = "FinancialYear", Label = "2025-26", Value = "2025-26", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "FinancialYear", Label = "2026-27", Value = "2026-27", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "FinancialYear", Label = "2027-28", Value = "2027-28", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Designations
        new() { MasterType = "Designation", Label = "Fellow", Value = "Fellow", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Designation", Label = "Coordinator", Value = "Coordinator", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Designation", Label = "Intern", Value = "Intern", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Designation", Label = "Admin", Value = "Admin", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "Designation", Label = "Team Lead", Value = "Team Lead", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },

        // Issue Categories
        new() { MasterType = "IssueCategory", Label = "Login Issue", Value = "Login Issue", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "IssueCategory", Label = "Attendance Issue", Value = "Attendance Issue", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "IssueCategory", Label = "Survey Issue", Value = "Survey Issue", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "IssueCategory", Label = "Technical Issue", Value = "Technical Issue", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { MasterType = "IssueCategory", Label = "Other", Value = "Other", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },
    ];

    dbContext.LookupMasters.AddRange(masters);
    await dbContext.SaveChangesAsync();
}

static async Task SeedModuleMaster(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    bool hasChildren = await dbContext.ModuleMasters.AnyAsync(mm => mm.ParentModuleMasterId != null);
    if (hasChildren)
    {
        return;
    }

    bool parentsExist = await dbContext.ModuleMasters.AnyAsync(mm => mm.ParentModuleMasterId == null);
    if (!parentsExist)
    {
        List<ModuleMaster> modules =
        [
            new() { ModuleCode = "REGISTRATION", ModuleName = "User Registration & Authentication", SortOrder = 1, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "TRAINING", ModuleName = "Training Management System", SortOrder = 2, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "WORK_ALLOCATION", ModuleName = "Work Allocation & Task Management", SortOrder = 3, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "ATTENDANCE", ModuleName = "Attendance & Leave Management", SortOrder = 4, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "PERFORMANCE", ModuleName = "Monitoring, Evaluation & Performance", SortOrder = 5, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "CERTIFICATE", ModuleName = "Certificate & Exit Management", SortOrder = 6, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "HELP_DESK", ModuleName = "Communication & Help Desk", SortOrder = 7, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "MASTERS", ModuleName = "Master Data Management", SortOrder = 8, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "DASHBOARD", ModuleName = "Dashboard & Analytics", SortOrder = 9, IsActive = true, CreatedOn = DateTime.UtcNow },
            new() { ModuleCode = "ADMINISTRATION", ModuleName = "System Administration", SortOrder = 10, IsActive = true, CreatedOn = DateTime.UtcNow },
        ];

        dbContext.ModuleMasters.AddRange(modules);
        await dbContext.SaveChangesAsync();
    }

    List<ModuleMaster> parents = await dbContext.ModuleMasters
        .Where(mm => mm.ParentModuleMasterId == null && mm.IsActive)
        .OrderBy(mm => mm.SortOrder)
        .ToListAsync();

    int parentReg = parents.First(m => m.ModuleCode == "REGISTRATION").ModuleMasterId;
    int parentTrain = parents.First(m => m.ModuleCode == "TRAINING").ModuleMasterId;
    int parentWork = parents.First(m => m.ModuleCode == "WORK_ALLOCATION").ModuleMasterId;
    int parentAttend = parents.First(m => m.ModuleCode == "ATTENDANCE").ModuleMasterId;
    int parentPerf = parents.First(m => m.ModuleCode == "PERFORMANCE").ModuleMasterId;
    int parentCert = parents.First(m => m.ModuleCode == "CERTIFICATE").ModuleMasterId;
    int parentHelp = parents.First(m => m.ModuleCode == "HELP_DESK").ModuleMasterId;
    int parentMasters = parents.First(m => m.ModuleCode == "MASTERS").ModuleMasterId;
    int parentDash = parents.First(m => m.ModuleCode == "DASHBOARD").ModuleMasterId;
    int parentAdmin = parents.First(m => m.ModuleCode == "ADMINISTRATION").ModuleMasterId;

    List<ModuleMaster> children =
    [
        new() { ModuleCode = "REG_REGISTRATIONS", ModuleName = "Registrations", SortOrder = 1, ParentModuleMasterId = parentReg, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "REG_USER_MGMT", ModuleName = "User Management", SortOrder = 2, ParentModuleMasterId = parentReg, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "TRN_TRAINING", ModuleName = "Training Sessions", SortOrder = 1, ParentModuleMasterId = parentTrain, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "TRN_MEETINGS", ModuleName = "Meeting Schedule", SortOrder = 2, ParentModuleMasterId = parentTrain, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "WA_ALLOCATIONS", ModuleName = "Work Allocations", SortOrder = 1, ParentModuleMasterId = parentWork, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "WA_TASKS", ModuleName = "Task Progress", SortOrder = 2, ParentModuleMasterId = parentWork, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "WA_SURVEYS", ModuleName = "Survey Records", SortOrder = 3, ParentModuleMasterId = parentWork, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "ATT_MARK", ModuleName = "Mark Attendance", SortOrder = 1, ParentModuleMasterId = parentAttend, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ATT_HOLIDAYS", ModuleName = "Holiday Calendar", SortOrder = 2, ParentModuleMasterId = parentAttend, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ATT_LEAVE", ModuleName = "Apply Leave", SortOrder = 3, ParentModuleMasterId = parentAttend, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ATT_LEAVE_STATUS", ModuleName = "Leave Status", SortOrder = 4, ParentModuleMasterId = parentAttend, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ATT_LEAVE_BALANCE", ModuleName = "Leave Balance", SortOrder = 5, ParentModuleMasterId = parentAttend, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "PERF_EVALUATION", ModuleName = "Performance Evaluation", SortOrder = 1, ParentModuleMasterId = parentPerf, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "CERT_QUEUE", ModuleName = "Certificate Queue", SortOrder = 1, ParentModuleMasterId = parentCert, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "CERT_APPLY", ModuleName = "Apply for Certificate", SortOrder = 2, ParentModuleMasterId = parentCert, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "CERT_EXIT", ModuleName = "Exit Management", SortOrder = 3, ParentModuleMasterId = parentCert, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "HD_TICKETS", ModuleName = "Help Desk Tickets", SortOrder = 1, ParentModuleMasterId = parentHelp, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "MAS_LOCATIONS", ModuleName = "Locations", SortOrder = 1, ParentModuleMasterId = parentMasters, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "MAS_PROJECTS", ModuleName = "Projects", SortOrder = 2, ParentModuleMasterId = parentMasters, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "MAS_WORKS", ModuleName = "Works", SortOrder = 3, ParentModuleMasterId = parentMasters, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "MAS_TRAINING", ModuleName = "Training Schedule", SortOrder = 4, ParentModuleMasterId = parentMasters, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "DASH_OVERVIEW", ModuleName = "Dashboard Overview", SortOrder = 1, ParentModuleMasterId = parentDash, IsActive = true, CreatedOn = DateTime.UtcNow },

        new() { ModuleCode = "ADM_USER_MGMT", ModuleName = "User Management", SortOrder = 1, ParentModuleMasterId = parentAdmin, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ADM_ACCESS", ModuleName = "User Access Management", SortOrder = 2, ParentModuleMasterId = parentAdmin, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ADM_AUDIT", ModuleName = "Access Audit Log", SortOrder = 3, ParentModuleMasterId = parentAdmin, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ADM_DOCS", ModuleName = "Document Verification", SortOrder = 4, ParentModuleMasterId = parentAdmin, IsActive = true, CreatedOn = DateTime.UtcNow },
        new() { ModuleCode = "ADM_SEED", ModuleName = "Seed Data", SortOrder = 5, ParentModuleMasterId = parentAdmin, IsActive = true, CreatedOn = DateTime.UtcNow },
    ];

    dbContext.ModuleMasters.AddRange(children);
    await dbContext.SaveChangesAsync();
}

static async Task SeedAdminModuleAccess(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    UserAccount? admin = await dbContext.UserAccounts
        .FirstOrDefaultAsync(ua => ua.Username == "admin");

    if (admin == null)
    {
        return;
    }

    bool accessExists = await dbContext.UserModuleAccesses
        .AnyAsync(uma => uma.UserAccountId == admin.UserAccountId);

    if (accessExists)
    {
        return;
    }

    List<ModuleMaster> modules = await dbContext.ModuleMasters
        .Where(mm => mm.IsActive)
        .ToListAsync();

    bool adminAccessExists = await dbContext.UserModuleAccesses
        .AnyAsync(uma => uma.UserAccountId == admin.UserAccountId);

    if (!adminAccessExists)
    {
        List<UserModuleAccess> adminAccess = modules.Select(mm => new UserModuleAccess
        {
            UserAccountId = admin.UserAccountId,
            ModuleMasterId = mm.ModuleMasterId,
            CanRead = true,
            CanWrite = true,
            CanApprove = true,
            CanExport = true,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = admin.UserAccountId,
        }).ToList();

        dbContext.UserModuleAccesses.AddRange(adminAccess);
        await dbContext.SaveChangesAsync();
    }
    else
    {
        List<int> existingModuleIds = await dbContext.UserModuleAccesses
            .Where(uma => uma.UserAccountId == admin.UserAccountId)
            .Select(uma => uma.ModuleMasterId)
            .ToListAsync();

        List<int> newModuleIds = modules
            .Where(mm => !existingModuleIds.Contains(mm.ModuleMasterId))
            .Select(mm => mm.ModuleMasterId)
            .ToList();

        if (newModuleIds.Count > 0)
        {
            List<UserModuleAccess> newAccess = newModuleIds.Select(moduleId => new UserModuleAccess
            {
                UserAccountId = admin.UserAccountId,
                ModuleMasterId = moduleId,
                CanRead = true,
                CanWrite = true,
                CanApprove = true,
                CanExport = true,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = admin.UserAccountId,
            }).ToList();

            dbContext.UserModuleAccesses.AddRange(newAccess);
            await dbContext.SaveChangesAsync();
        }
    }
}

static async Task SeedAdminUserRole(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    UserAccount? admin = await dbContext.UserAccounts
        .FirstOrDefaultAsync(ua => ua.Username == "admin");

    if (admin == null)
    {
        return;
    }

    bool roleExists = await dbContext.UserRoles
        .AnyAsync(ur => ur.UserAccountId == admin.UserAccountId);

    if (roleExists)
    {
        return;
    }

    dbContext.UserRoles.Add(new UserRole
    {
        UserAccountId = admin.UserAccountId,
        RoleLookupId = 1,
        IsActive = true,
        CreatedOn = DateTime.UtcNow,
        CreatedBy = admin.UserAccountId,
    });

    await dbContext.SaveChangesAsync();
}

static async Task SeedLeaveTypes(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    bool anyExists = await dbContext.LeaveTypes.AnyAsync();
    if (anyExists)
    {
        return;
    }

    List<LeaveType> leaveTypes =
    [
        new() { TypeName = "Casual Leave", Code = "CL", DefaultDays = 8, IsActive = true, SortOrder = 1, CreatedOn = DateTime.UtcNow },
        new() { TypeName = "Medical Leave", Code = "ML", DefaultDays = 12, IsActive = true, SortOrder = 2, CreatedOn = DateTime.UtcNow },
        new() { TypeName = "Earned Leave", Code = "EL", DefaultDays = 15, IsActive = true, SortOrder = 3, CreatedOn = DateTime.UtcNow },
        new() { TypeName = "Leave Without Pay", Code = "LWP", DefaultDays = 0, IsActive = true, SortOrder = 4, CreatedOn = DateTime.UtcNow },
    ];

    dbContext.LeaveTypes.AddRange(leaveTypes);
    await dbContext.SaveChangesAsync();
}

static async Task SeedTicketCategories(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    bool anyExists = await dbContext.TicketCategories.AnyAsync();
    if (anyExists)
    {
        return;
    }

    List<TicketCategory> categories =
    [
        new() { CategoryName = "Login Issue", Description = "Problems related to login and authentication", DefaultPriority = "High", IsActive = true, SortOrder = 1, CreatedOn = DateTime.UtcNow },
        new() { CategoryName = "Attendance Issue", Description = "Problems related to attendance marking or records", DefaultPriority = "Medium", IsActive = true, SortOrder = 2, CreatedOn = DateTime.UtcNow },
        new() { CategoryName = "Survey Issue", Description = "Problems related to survey forms or submissions", DefaultPriority = "Medium", IsActive = true, SortOrder = 3, CreatedOn = DateTime.UtcNow },
        new() { CategoryName = "Technical Issue", Description = "General technical problems or bugs", DefaultPriority = "Medium", IsActive = true, SortOrder = 4, CreatedOn = DateTime.UtcNow },
        new() { CategoryName = "Payment Issue", Description = "Problems related to payments or financial transactions", DefaultPriority = "High", IsActive = true, SortOrder = 5, CreatedOn = DateTime.UtcNow },
        new() { CategoryName = "Other", Description = "Any other issues not covered above", DefaultPriority = "Low", IsActive = true, SortOrder = 6, CreatedOn = DateTime.UtcNow },
    ];

    dbContext.TicketCategories.AddRange(categories);
    await dbContext.SaveChangesAsync();
}
