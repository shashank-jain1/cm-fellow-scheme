using System.Security.Cryptography;
using System.Text;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using CmScheme.Dashboard.Infrastructure;

using CmScheme.Common.Core.Services;
using CmScheme.Api.Authorization;
using CmScheme.Api.Services;
using Microsoft.AspNetCore.Authorization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSingleton<IAuthorizationHandler, ModuleAuthorizationHandler>();

builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
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
    .AddDashboardApis().AddDashboardServices(connectionString).AddDashboardInfrastructure();

builder.Services.AddScoped<INotificationService, StubNotificationService>();
builder.Services.AddSingleton<IBusinessKeyGenerator, BusinessKeyGenerator>();

WebApplication app = builder.Build();

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
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapApiEndpoints("/api/v1");

await SeedAdminUser(app);
await SeedLookupMasters(app);
await SeedModuleMaster(app);
await SeedAdminModuleAccess(app);
await LocationSeedData.SeedAsync(app.Services.CreateScope().ServiceProvider.GetRequiredService<IMastersCommandDbContext>());

app.Run();

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
    ];

    dbContext.LookupMasters.AddRange(masters);
    await dbContext.SaveChangesAsync();
}

static async Task SeedModuleMaster(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    IRegistrationCommandDbContext dbContext = scope.ServiceProvider
        .GetRequiredService<IRegistrationCommandDbContext>();

    bool anyExists = await dbContext.ModuleMasters.AnyAsync();
    if (anyExists)
    {
        return;
    }

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
