namespace CmScheme.Registration.Core.Dtos;

public sealed record ApplicantDto(
    int ApplicantId,
    string FirstName,
    string? MiddleName,
    string LastName,
    string FatherName,
    string MobileNumber,
    string EmailId,
    DateTime DateOfBirth,
    string Status,
    string? FullName);
