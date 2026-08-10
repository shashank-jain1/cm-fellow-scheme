using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Application.Features.Registration.SubmitRegistration;
using CmScheme.Registration.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CmScheme.Tests.Registration.Handlers;

public class SubmitRegistrationCommandHandlerTests
{
    private static SubmitRegistrationCommandHandler CreateHandler(
        RegistrationCommandDbContext ctx,
        IFileUploadService fileUploadService) => new(ctx, fileUploadService);

    private static SubmitRegistrationCommand BuildCommand() => new()
    {
        FirstName = "Rohit",
        MiddleName = "K",
        LastName = "Sharma",
        FatherName = "Kishan",
        AadhaarNumber = "123456789012",
        MobileNumber = "9876543210",
        EmailId = "rohit@example.com",
        DateOfBirth = new DateTime(2000, 1, 1),
        PermanentAddress = "Mumbai",
        DivisionId = 1,
        DistrictId = 1,
        BlockId = 1,
        GramPanchayatId = 1,
        PinCode = "400001",
        AppliedForTraining = 1,
        PreferredTrainingLocationId = 1,
        QualificationId = 1,
        BoardUniversityName = "MU",
        PassingYear = 2021,
        PercentageCGPA = 8.5m,
        ExperienceDetails = "2 yrs",
        DeclarationAccepted = true
    };

    [Fact]
    public async Task Handle_Valid_Command_Creates_Applicant_And_Returns_Id()
    {
        var ctx = TestDbContext.CreateRegistration();
        var upload = new Mock<IFileUploadService>();
        var handler = CreateHandler(ctx, upload.Object);

        var result = await handler.Handle(BuildCommand(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.Applicants.CountAsync());
        var applicant = ctx.Applicants.Single();
        Assert.Equal("Rohit", applicant.FirstName);
        Assert.Equal("rohit@example.com", applicant.EmailId);
        Assert.Equal(Statuses.Registration.Pending, applicant.Status);
        Assert.True(applicant.DeclarationAccepted);
        Assert.Null(applicant.PhotographPath);
        upload.Verify(u => u.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_With_Photograph_Uploads_File_And_Stores_Path()
    {
        var ctx = TestDbContext.CreateRegistration();
        var upload = new Mock<IFileUploadService>();
        upload.Setup(u => u.UploadAsync(It.IsAny<Stream>(), "photo.jpg", "image/jpeg", "documents/photographs", It.IsAny<CancellationToken>()))
            .ReturnsAsync("documents/photographs/photo-1.jpg");
        var handler = CreateHandler(ctx, upload.Object);

        using var ms = new MemoryStream(new byte[] { 1, 2, 3 });
        var photoMock = new Mock<IFormFile>();
        photoMock.Setup(f => f.OpenReadStream()).Returns(ms);
        photoMock.Setup(f => f.FileName).Returns("photo.jpg");
        photoMock.Setup(f => f.ContentType).Returns("image/jpeg");
        photoMock.Setup(f => f.Length).Returns(ms.Length);
        var command = BuildCommand();
        command = command with { Photograph = photoMock.Object };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        var applicant = ctx.Applicants.Single();
        Assert.Equal("documents/photographs/photo-1.jpg", applicant.PhotographPath);
        upload.Verify(u => u.UploadAsync(It.IsAny<Stream>(), "photo.jpg", "image/jpeg", "documents/photographs", It.IsAny<CancellationToken>()), Times.Once);
    }
}