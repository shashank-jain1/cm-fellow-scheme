using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CmScheme.Endpoints.Abstractions.ApiErrors;

public sealed class ApiProblemDetails : ProblemDetails, IApiProblemDetails
{
    public ApiProblemDetails(ErrorCode errorCode, HttpStatusCode status, string title, string? detail = null)
    {
        Code = errorCode;
        Status = (int)status;
        Title = title;
        Detail = detail;
    }
    public ErrorCode Code { get; }
}
