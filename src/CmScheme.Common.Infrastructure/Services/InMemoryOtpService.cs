using System.Collections.Concurrent;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using CmScheme.Common.Core.Services;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class InMemoryOtpService(ILogger<InMemoryOtpService> logger) : IOtpService
{
    private static readonly TimeSpan OtpExpiry = TimeSpan.FromMinutes(5);

    private readonly ConcurrentDictionary<string, (string Otp, DateTime Expiry)> _otpStore = new();

    public Task<string> GenerateOtpAsync(string identifier, CancellationToken ct = default)
    {
        string otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        _otpStore[identifier] = (otp, DateTime.UtcNow.Add(OtpExpiry));

        logger.LogInformation("OTP generated for {Identifier}", identifier);

        return Task.FromResult(otp);
    }

    public Task<bool> VerifyOtpAsync(string identifier, string otp, CancellationToken ct = default)
    {
        if (!_otpStore.TryGetValue(identifier, out (string Otp, DateTime Expiry) stored))
        {
            logger.LogWarning("OTP not found for {Identifier}", identifier);
            return Task.FromResult(false);
        }

        if (DateTime.UtcNow > stored.Expiry)
        {
            _otpStore.TryRemove(identifier, out _);
            logger.LogWarning("OTP expired for {Identifier}", identifier);
            return Task.FromResult(false);
        }

        if (!string.Equals(stored.Otp, otp, StringComparison.Ordinal))
        {
            logger.LogWarning("Invalid OTP for {Identifier}", identifier);
            return Task.FromResult(false);
        }

        _otpStore.TryRemove(identifier, out _);

        logger.LogInformation("OTP verified successfully for {Identifier}", identifier);
        return Task.FromResult(true);
    }
}
