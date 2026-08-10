using CmScheme.AttendanceLeave.Infrastructure;
using CmScheme.Certificate.Infrastructure;
using CmScheme.Dashboard.Infrastructure;
using CmScheme.HelpDesk.Infrastructure;
using CmScheme.Masters.Infrastructure.Data;
using CmScheme.Performance.Infrastructure;
using CmScheme.Registration.Infrastructure;
using CmScheme.Training.Infrastructure.Data;
using CmScheme.WorkAllocation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests;

/// <summary>
/// Builds in-memory DbContexts for tests. Handlers depend on the Command/Query
/// interfaces, so a single concrete instance that implements them is provided.
/// Multi-context modules (Masters, Registration, Training) use their Command context.
/// </summary>
public static class TestDbContext
{
    public static AttendanceLeaveDbContext CreateAttendanceLeave(Action<AttendanceLeaveDbContext>? seed = null)
    {
        DbContextOptions<AttendanceLeaveDbContext> options = new DbContextOptionsBuilder<AttendanceLeaveDbContext>()
            .UseInMemoryDatabase($"attendance-{Guid.NewGuid():N}")
            .Options;

        AttendanceLeaveDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static WorkAllocationDbContext CreateWorkAllocation(Action<WorkAllocationDbContext>? seed = null)
    {
        DbContextOptions<WorkAllocationDbContext> options = new DbContextOptionsBuilder<WorkAllocationDbContext>()
            .UseInMemoryDatabase($"workallocation-{Guid.NewGuid():N}")
            .Options;

        WorkAllocationDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static CertificateDbContext CreateCertificate(Action<CertificateDbContext>? seed = null)
    {
        DbContextOptions<CertificateDbContext> options = new DbContextOptionsBuilder<CertificateDbContext>()
            .UseInMemoryDatabase($"certificate-{Guid.NewGuid():N}")
            .Options;

        CertificateDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static HelpDeskDbContext CreateHelpDesk(Action<HelpDeskDbContext>? seed = null)
    {
        DbContextOptions<HelpDeskDbContext> options = new DbContextOptionsBuilder<HelpDeskDbContext>()
            .UseInMemoryDatabase($"helpdesk-{Guid.NewGuid():N}")
            .Options;

        HelpDeskDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static DashboardDbContext CreateDashboard(Action<DashboardDbContext>? seed = null)
    {
        DbContextOptions<DashboardDbContext> options = new DbContextOptionsBuilder<DashboardDbContext>()
            .UseInMemoryDatabase($"dashboard-{Guid.NewGuid():N}")
            .Options;

        DashboardDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static PerformanceDbContext CreatePerformance(Action<PerformanceDbContext>? seed = null)
    {
        DbContextOptions<PerformanceDbContext> options = new DbContextOptionsBuilder<PerformanceDbContext>()
            .UseInMemoryDatabase($"performance-{Guid.NewGuid():N}")
            .Options;

        PerformanceDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static MastersCommandDbContext CreateMasters(Action<MastersCommandDbContext>? seed = null)
    {
        DbContextOptions<MastersCommandDbContext> options = new DbContextOptionsBuilder<MastersCommandDbContext>()
            .UseInMemoryDatabase($"masters-{Guid.NewGuid():N}")
            .Options;

        MastersCommandDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static RegistrationQueryDbContext CreateRegistrationQuery(Action<RegistrationQueryDbContext>? seed = null)
    {
        DbContextOptions<RegistrationQueryDbContext> options = new DbContextOptionsBuilder<RegistrationQueryDbContext>()
            .UseInMemoryDatabase($"registration-query-{Guid.NewGuid():N}")
            .Options;

        RegistrationQueryDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static RegistrationCommandDbContext CreateRegistration(Action<RegistrationCommandDbContext>? seed = null)
    {
        DbContextOptions<RegistrationCommandDbContext> options = new DbContextOptionsBuilder<RegistrationCommandDbContext>()
            .UseInMemoryDatabase($"registration-{Guid.NewGuid():N}")
            .Options;

        RegistrationCommandDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static TrainingCommandDbContext CreateTraining(Action<TrainingCommandDbContext>? seed = null)
    {
        DbContextOptions<TrainingCommandDbContext> options = new DbContextOptionsBuilder<TrainingCommandDbContext>()
            .UseInMemoryDatabase($"training-{Guid.NewGuid():N}")
            .Options;

        TrainingCommandDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static TrainingQueryDbContext CreateTrainingQuery(Action<TrainingQueryDbContext>? seed = null)
    {
        DbContextOptions<TrainingQueryDbContext> options = new DbContextOptionsBuilder<TrainingQueryDbContext>()
            .UseInMemoryDatabase($"training-query-{Guid.NewGuid():N}")
            .Options;

        TrainingQueryDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }

    public static MastersQueryDbContext CreateMastersQuery(Action<MastersQueryDbContext>? seed = null)
    {
        DbContextOptions<MastersQueryDbContext> options = new DbContextOptionsBuilder<MastersQueryDbContext>()
            .UseInMemoryDatabase($"masters-query-{Guid.NewGuid():N}")
            .Options;

        MastersQueryDbContext context = new(options);
        seed?.Invoke(context);
        return context;
    }
}