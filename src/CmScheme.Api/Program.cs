using System.Security.Cryptography;
using System.Text;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CmScheme.Common.Core.Behaviours;
using CmScheme.Common.Core.Data;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Masters.Endpoints;
using CmScheme.Masters.Infrastructure;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Endpoints;
using CmScheme.Registration.Infrastructure;
using CmScheme.Training.Endpoints;
using CmScheme.Training.Infrastructure;
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

builder.Services.AddAuthorization();

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

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints("/api/v1");

await SeedAdminUser(app);

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
