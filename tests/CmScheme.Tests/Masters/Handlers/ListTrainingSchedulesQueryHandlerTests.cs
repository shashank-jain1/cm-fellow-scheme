using CmScheme.Masters.Application.Features.TrainingSchedules.ListTrainingSchedules;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListTrainingSchedulesQueryHandlerTests
{
    private static ListTrainingSchedulesQueryHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Returns_All_Schedules_With_Project_Name()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            var project = new Project
            {
                ProjectName = "Rural Roads",
                ProjectCode = "RR",
                DepartmentName = "PWD",
                ProjectIncharge = "A",
                BudgetApprovedBy = 1
            };
            c.Projects.Add(project);
            c.TrainingSchedules.AddRange(
                new TrainingSchedule
                {
                    CalendarYear = "2026",
                    Project = project,
                    TrainingDate = new DateTime(2026, 6, 1)
                },
                new TrainingSchedule
                {
                    CalendarYear = "2027",
                    Project = project,
                    TrainingDate = new DateTime(2027, 6, 1)
                });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListTrainingSchedulesQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, ts => Assert.Equal("Rural Roads", ts.ProjectName));
    }

    [Fact]
    public async Task Handle_With_CalendarYear_Filter_Returns_Matching()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            var project = new Project
            {
                ProjectName = "Rural Roads",
                ProjectCode = "RR",
                DepartmentName = "PWD",
                ProjectIncharge = "A",
                BudgetApprovedBy = 1
            };
            c.Projects.Add(project);
            c.TrainingSchedules.AddRange(
                new TrainingSchedule
                {
                    CalendarYear = "2026",
                    Project = project,
                    TrainingDate = new DateTime(2026, 6, 1)
                },
                new TrainingSchedule
                {
                    CalendarYear = "2027",
                    Project = project,
                    TrainingDate = new DateTime(2027, 6, 1)
                });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListTrainingSchedulesQuery { CalendarYear = "2026" }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.Equal("2026", result.Value[0].CalendarYear);
    }

    [Fact]
    public async Task Handle_With_ProjectId_And_DivisionId_Filters()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            var project1 = new Project
            {
                ProjectName = "Roads",
                ProjectCode = "R1",
                DepartmentName = "PWD",
                ProjectIncharge = "A",
                BudgetApprovedBy = 1
            };
            var project2 = new Project
            {
                ProjectName = "Water",
                ProjectCode = "W1",
                DepartmentName = "PWD",
                ProjectIncharge = "B",
                BudgetApprovedBy = 1
            };
            c.Projects.AddRange(project1, project2);
            c.TrainingSchedules.AddRange(
                new TrainingSchedule
                {
                    CalendarYear = "2026",
                    Project = project1,
                    DivisionId = 1,
                    TrainingDate = new DateTime(2026, 6, 1)
                },
                new TrainingSchedule
                {
                    CalendarYear = "2026",
                    Project = project1,
                    DivisionId = 2,
                    TrainingDate = new DateTime(2026, 6, 2)
                },
                new TrainingSchedule
                {
                    CalendarYear = "2026",
                    Project = project2,
                    DivisionId = 1,
                    TrainingDate = new DateTime(2026, 6, 3)
                });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListTrainingSchedulesQuery { ProjectId = 1, DivisionId = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.Equal(1, result.Value[0].ProjectId);
        Assert.Equal(1, result.Value[0].DivisionId);
    }

    [Fact]
    public async Task Handle_No_Matching_Schedules_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            var project = new Project
            {
                ProjectName = "Rural Roads",
                ProjectCode = "RR",
                DepartmentName = "PWD",
                ProjectIncharge = "A",
                BudgetApprovedBy = 1
            };
            c.Projects.Add(project);
            c.TrainingSchedules.Add(new TrainingSchedule
            {
                CalendarYear = "2026",
                Project = project,
                TrainingDate = new DateTime(2026, 6, 1)
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListTrainingSchedulesQuery { CalendarYear = "2030" }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}