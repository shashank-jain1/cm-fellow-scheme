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

public sealed record RegistrationListItem(
    int ApplicantId,
    string FirstName,
    string LastName,
    string MobileNumber,
    string EmailId,
    string Status,
    DateTime CreatedOn);
