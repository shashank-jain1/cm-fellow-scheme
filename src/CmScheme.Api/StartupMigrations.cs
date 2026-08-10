using Microsoft.EntityFrameworkCore;

namespace CmScheme.Api;

internal static class StartupMigrations
{
    public static async Task ApplyAsync(WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider sp = scope.ServiceProvider;

        (string Name, DbContext Context)[] contexts =
        [
            ("Masters", sp.GetRequiredService<Masters.Infrastructure.Data.MastersCommandDbContext>()),
            ("Registration", sp.GetRequiredService<Registration.Infrastructure.RegistrationCommandDbContext>()),
            ("Training", sp.GetRequiredService<Training.Infrastructure.Data.TrainingCommandDbContext>()),
            ("WorkAllocation", sp.GetRequiredService<WorkAllocation.Infrastructure.WorkAllocationDbContext>()),
            ("AttendanceLeave", sp.GetRequiredService<AttendanceLeave.Infrastructure.AttendanceLeaveDbContext>()),
            ("Certificate", sp.GetRequiredService<Certificate.Infrastructure.CertificateDbContext>()),
            ("HelpDesk", sp.GetRequiredService<HelpDesk.Infrastructure.HelpDeskDbContext>()),
            ("Performance", sp.GetRequiredService<Performance.Infrastructure.PerformanceDbContext>()),
            ("Dashboard", sp.GetRequiredService<Dashboard.Infrastructure.DashboardDbContext>()),
        ];

        ILogger logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(StartupMigrations));

        foreach ((string Name, DbContext Context) in contexts)
        {
            try
            {
                await Context.Database.MigrateAsync();
            }
            catch (Exception migrateEx)
            {
                logger.LogWarning(migrateEx,
                    "Migrating {Context} failed; falling back to EnsureCreated.", Name);

                try
                {
                    await Context.Database.EnsureCreatedAsync();
                }
                catch (Exception ensureEx)
                {
                    logger.LogError(ensureEx,
                        "Could not initialise the {Context} schema. Endpoints backed by it will fail.", Name);
                }
            }
        }

        try
        {
            Masters.Infrastructure.Data.MastersCommandDbContext mastersCtx =
                sp.GetRequiredService<Masters.Infrastructure.Data.MastersCommandDbContext>();
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
}
