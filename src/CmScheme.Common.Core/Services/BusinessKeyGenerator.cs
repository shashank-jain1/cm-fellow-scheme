namespace CmScheme.Common.Core.Services;

public interface IBusinessKeyGenerator
{
    string GenerateCertificateKey(int sequenceNumber);
    string GenerateTicketKey(int sequenceNumber);
    string GenerateRegistrationKey(int sequenceNumber);
}

public sealed class BusinessKeyGenerator : IBusinessKeyGenerator
{
    public string GenerateCertificateKey(int sequenceNumber)
    {
        int year = DateTime.UtcNow.Year;
        return $"CERT{year}{sequenceNumber:D4}";
    }

    public string GenerateTicketKey(int sequenceNumber)
    {
        int year = DateTime.UtcNow.Year;
        return $"TKT{year}{sequenceNumber:D4}";
    }

    public string GenerateRegistrationKey(int sequenceNumber)
    {
        int year = DateTime.UtcNow.Year;
        return $"REG{year}{sequenceNumber:D4}";
    }
}
