namespace CmScheme.Endpoints.Abstractions.ApiErrors;

public interface IApiProblemDetails
{
    ErrorCode Code { get; }
}
