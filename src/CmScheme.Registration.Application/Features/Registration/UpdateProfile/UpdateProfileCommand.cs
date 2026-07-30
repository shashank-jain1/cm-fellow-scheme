using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.UpdateProfile;

public sealed record UpdateProfileCommand : ICommand<Result>
{
    public int ApplicantId { get; init; }
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;
    public string MobileNumber { get; init; } = null!;
    public string EmailId { get; init; } = null!;
    public string PermanentAddress { get; init; } = null!;
    public int QualificationId { get; init; }
    public string? ExperienceDetails { get; init; }
}
