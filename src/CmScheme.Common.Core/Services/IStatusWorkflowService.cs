namespace CmScheme.Common.Core.Services;

public interface IStatusWorkflowService
{
    bool CanTransition(string currentStatus, string targetStatus);
    IReadOnlyList<string> GetAllowedTransitions(string currentStatus);
}
