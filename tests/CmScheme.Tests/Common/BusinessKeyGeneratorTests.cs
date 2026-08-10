using CmScheme.Common.Core.Services;

namespace CmScheme.Tests.Common;

public class BusinessKeyGeneratorTests
{
    private readonly BusinessKeyGenerator _generator = new();

    [Fact]
    public void GenerateCertificateKey_Has_Cert_Prefix_And_CurrentYear()
    {
        string key = _generator.GenerateCertificateKey(1);
        Assert.StartsWith($"CERT{DateTime.UtcNow.Year}", key);
    }

    [Fact]
    public void GenerateCertificateKey_Zero_Pads_Sequence_To_Four_Digits()
    {
        string key = _generator.GenerateCertificateKey(7);
        Assert.Equal($"CERT{DateTime.UtcNow.Year}0007", key);
    }

    [Fact]
    public void GenerateCertificateKey_Handles_Large_Sequence()
    {
        string key = _generator.GenerateCertificateKey(12345);
        Assert.EndsWith("12345", key);
    }

    [Fact]
    public void GenerateTicketKey_Has_Tkt_Prefix_And_CurrentYear()
    {
        string key = _generator.GenerateTicketKey(42);
        Assert.Equal($"TKT{DateTime.UtcNow.Year}0042", key);
    }

    [Fact]
    public void GenerateRegistrationKey_Has_Reg_Prefix_And_CurrentYear()
    {
        string key = _generator.GenerateRegistrationKey(99);
        Assert.Equal($"REG{DateTime.UtcNow.Year}0099", key);
    }

    [Fact]
    public void GenerateRegistrationKey_Zero_Sequence_Is_Accepted()
    {
        string key = _generator.GenerateRegistrationKey(0);
        Assert.Equal($"REG{DateTime.UtcNow.Year}0000", key);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(999)]
    [InlineData(4096)]
    public void Generated_Keys_Are_Unique_Per_Type(int sequenceNumber)
    {
        string certificate = _generator.GenerateCertificateKey(sequenceNumber);
        string ticket = _generator.GenerateTicketKey(sequenceNumber);
        string registration = _generator.GenerateRegistrationKey(sequenceNumber);

        Assert.NotEqual(certificate, ticket);
        Assert.NotEqual(certificate, registration);
        Assert.NotEqual(ticket, registration);
    }
}