using CmScheme.Common.Core.Services;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class StatusWorkflowService : IStatusWorkflowService
{
    private static readonly IReadOnlyDictionary<string, IReadOnlyList<string>> RegistrationTransitions =
        new Dictionary<string, IReadOnlyList<string>>
        {
            ["Submitted"] = new List<string> { "Under Review", "Rejected" },
            ["Under Review"] = new List<string> { "Approved", "Rejected", "Additional Info Required" },
            ["Additional Info Required"] = new List<string> { "Under Review", "Rejected" },
            ["Approved"] = Array.Empty<string>(),
            ["Rejected"] = Array.Empty<string>(),
        };

    public bool CanTransition(string currentStatus, string targetStatus)
    {
        IReadOnlyList<string> allowed = GetAllowedTransitions(currentStatus);
        return allowed.Contains(targetStatus);
    }

    public IReadOnlyList<string> GetAllowedTransitions(string currentStatus)
    {
        return RegistrationTransitions.TryGetValue(currentStatus, out IReadOnlyList<string>? transitions)
            ? transitions
            : Array.Empty<string>();
    }
}
