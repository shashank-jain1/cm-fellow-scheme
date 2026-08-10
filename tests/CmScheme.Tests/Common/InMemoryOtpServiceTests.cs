using CmScheme.Common.Infrastructure.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace CmScheme.Tests.Common;

public class InMemoryOtpServiceTests
{
    private static InMemoryOtpService CreateService() =>
        new(NullLogger<InMemoryOtpService>.Instance);

    [Fact]
    public async Task GenerateOtpAsync_Returns_Six_Digit_Numeric_Otp()
    {
        InMemoryOtpService service = CreateService();

        string otp = await service.GenerateOtpAsync("fellow@test.gov.in");

        Assert.Equal(6, otp.Length);
        Assert.All(otp, c => Assert.True(char.IsDigit(c), $"'{c}' is not a digit"));
    }

    [Fact]
    public async Task GenerateOtpAsync_Stores_Otp_Per_Identifier()
    {
        InMemoryOtpService service = CreateService();

        string first = await service.GenerateOtpAsync("alice@test.gov.in");
        string second = await service.GenerateOtpAsync("bob@test.gov.in");

        bool aliceOk = await service.VerifyOtpAsync("alice@test.gov.in", first);
        bool bobOk = await service.VerifyOtpAsync("bob@test.gov.in", second);

        Assert.True(aliceOk);
        Assert.True(bobOk);
    }

    [Fact]
    public async Task VerifyOtpAsync_With_Correct_Otp_Returns_True()
    {
        InMemoryOtpService service = CreateService();
        string otp = await service.GenerateOtpAsync("fellow@test.gov.in");

        bool result = await service.VerifyOtpAsync("fellow@test.gov.in", otp);

        Assert.True(result);
    }

    [Fact]
    public async Task VerifyOtpAsync_With_Wrong_Otp_Returns_False()
    {
        InMemoryOtpService service = CreateService();
        await service.GenerateOtpAsync("fellow@test.gov.in");

        bool result = await service.VerifyOtpAsync("fellow@test.gov.in", "000000");

        Assert.False(result);
    }

    [Fact]
    public async Task VerifyOtpAsync_For_Unknown_Identifier_Returns_False()
    {
        InMemoryOtpService service = CreateService();

        bool result = await service.VerifyOtpAsync("nobody@test.gov.in", "123456");

        Assert.False(result);
    }

    [Fact]
    public async Task VerifyOtpAsync_Is_One_Time_Use()
    {
        InMemoryOtpService service = CreateService();
        string otp = await service.GenerateOtpAsync("fellow@test.gov.in");

        bool first = await service.VerifyOtpAsync("fellow@test.gov.in", otp);
        bool second = await service.VerifyOtpAsync("fellow@test.gov.in", otp);

        Assert.True(first);
        Assert.False(second);
    }

    [Fact]
    public async Task VerifyOtpAsync_Regenerated_Otp_Invalidates_Previous()
    {
        InMemoryOtpService service = CreateService();
        string first = await service.GenerateOtpAsync("fellow@test.gov.in");
        string second = await service.GenerateOtpAsync("fellow@test.gov.in");

        bool firstResult = await service.VerifyOtpAsync("fellow@test.gov.in", first);
        bool secondResult = await service.VerifyOtpAsync("fellow@test.gov.in", second);

        Assert.False(firstResult);
        Assert.True(secondResult);
    }

    [Fact]
    public async Task VerifyOtpAsync_Expired_Otp_Returns_False()
    {
        InMemoryOtpService service = CreateService();
        string otp = await service.GenerateOtpAsync("fellow@test.gov.in");

        var stored = service.GetType()
            .GetField("_otpStore", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .GetValue(service) as System.Collections.Concurrent.ConcurrentDictionary<string, (string Otp, DateTime Expiry)>;

        Assert.NotNull(stored);
        var entry = stored!["fellow@test.gov.in"];
        stored["fellow@test.gov.in"] = (entry.Otp, DateTime.UtcNow.AddMinutes(-1));

        bool result = await service.VerifyOtpAsync("fellow@test.gov.in", otp);

        Assert.False(result);
    }

    [Fact]
    public async Task GenerateOtpAsync_Supports_Cancellation_Token()
    {
        InMemoryOtpService service = CreateService();

        using var cts = new CancellationTokenSource();
        string otp = await service.GenerateOtpAsync("fellow@test.gov.in", cts.Token);

        Assert.Equal(6, otp.Length);
    }
}