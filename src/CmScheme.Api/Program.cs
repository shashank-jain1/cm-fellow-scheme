using Mediator;
using CmScheme.Common.Core.Behaviours;
using CmScheme.Common.Core.Data;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Masters.Endpoints;
using CmScheme.Masters.Infrastructure;
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

app.MapApiEndpoints("/api/v1");

app.Run();
