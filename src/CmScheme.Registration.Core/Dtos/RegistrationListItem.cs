namespace CmScheme.Registration.Core.Dtos;

public sealed record RegistrationListItem(
    int ApplicantId,
    string FirstName,
    string LastName,
    string MobileNumber,
    string EmailId,
    DateTime DateOfBirth,
    string Status,
    DateTime CreatedOn);
