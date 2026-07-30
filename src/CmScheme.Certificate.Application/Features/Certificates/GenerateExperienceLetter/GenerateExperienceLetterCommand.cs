using Ardalis.Result;
using Mediator;

namespace CmScheme.Certificate.Application.Features.Certificates.GenerateExperienceLetter;

public sealed record GenerateExperienceLetterCommand : ICommand<Result<string>>
{
    public int ApplicantId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string SupervisorName { get; init; } = null!;
}
