using CmScheme.Common.Core;
using CmScheme.Registration.Application.Features.Registration.ListRegistrations;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Infrastructure;

namespace CmScheme.Tests.Registration.Handlers;

public class ListRegistrationsQueryHandlerTests
{
    private static ListRegistrationsQueryHandler CreateHandler(RegistrationQueryDbContext ctx) => new(ctx);

    private static Applicant BuildApplicant(string firstName, string lastName, string mobile, string status, DateTime createdOn) => new()
    {
        FirstName = firstName,
        LastName = lastName,
        FatherName = "Father",
        MobileNumber = mobile,
        EmailId = $"{mobile}@example.com",
        PermanentAddress = "Mumbai",
        PinCode = "400001",
        BoardUniversityName = "MU",
        Status = status,
        CreatedOn = createdOn
    };

    private static RegistrationQueryDbContext BuildContext()
    {
        DateTime now = DateTime.UtcNow;
        return TestDbContext.CreateRegistrationQuery(c =>
        {
            c.Applicants.AddRange(
                BuildApplicant("Alpha", "One", "1000000001", Statuses.Registration.Pending, now.AddDays(-1)),
                BuildApplicant("Beta", "Two", "1000000002", Statuses.Registration.Approved, now),
                BuildApplicant("Gamma", "Three", "1000000003", Statuses.Registration.Pending, now.AddDays(-3)),
                BuildApplicant("Delta", "Four", "1000000004", Statuses.Registration.Rejected, now.AddDays(-2)));
            c.SaveChanges();
        });
    }

    [Fact]
    public async Task Handle_No_Filters_Returns_All_Ordered_By_CreatedOn_Desc()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListRegistrationsQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Value.Count);
        Assert.Equal("Beta", result.Value[0].FirstName);
        Assert.Equal("Alpha", result.Value[1].FirstName);
        Assert.Equal("Delta", result.Value[2].FirstName);
        Assert.Equal("Gamma", result.Value[3].FirstName);
    }

    [Fact]
    public async Task Handle_SearchTerm_Filters_By_Name_Or_Mobile()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListRegistrationsQuery { SearchTerm = "gamma" }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        var item = Assert.Single(result.Value);
        Assert.Equal("Gamma", item.FirstName);

        var byMobile = await handler.Handle(new ListRegistrationsQuery { SearchTerm = "1000000002" }, CancellationToken.None);
        Assert.Single(byMobile.Value);
        Assert.Equal("Beta", byMobile.Value[0].FirstName);
    }

    [Fact]
    public async Task Handle_Status_Filter_Returns_Matching_Only()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListRegistrationsQuery { Status = Statuses.Registration.Pending }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, i => Assert.Equal(Statuses.Registration.Pending, i.Status));
    }

    [Fact]
    public async Task Handle_Pagination_Respects_PageSize_And_PageNumber()
    {
        var ctx = BuildContext();
        var handler = CreateHandler(ctx);

        var page1 = await handler.Handle(new ListRegistrationsQuery { PageNumber = 1, PageSize = 2 }, CancellationToken.None);
        var page2 = await handler.Handle(new ListRegistrationsQuery { PageNumber = 2, PageSize = 2 }, CancellationToken.None);

        Assert.Equal(2, page1.Value.Count);
        Assert.Equal(2, page2.Value.Count);
        Assert.Equal("Beta", page1.Value[0].FirstName);
        Assert.Equal("Alpha", page1.Value[1].FirstName);
        Assert.Equal("Delta", page2.Value[0].FirstName);
        Assert.Equal("Gamma", page2.Value[1].FirstName);
    }

    [Fact]
    public async Task Handle_Empty_Store_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateRegistrationQuery();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListRegistrationsQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}