using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CmScheme.Api.Middleware;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        int statusCode = exception switch
        {
            BadHttpRequestException badRequest => badRequest.StatusCode,
            ArgumentException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        string title = statusCode switch
        {
            StatusCodes.Status400BadRequest => "Bad Request",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status404NotFound => "Not Found",
            StatusCodes.Status409Conflict => "Conflict",
            _ => "Internal Server Error",
        };

        // Client-side faults (missing/invalid route or query parameters) are expected traffic,
        // not server failures — log them at a level that does not pollute the error stream.
        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception,
                "Unhandled exception occurred. TraceId: {TraceId}, StatusCode: {StatusCode}",
                traceId, statusCode);
        }
        else
        {
            _logger.LogWarning(exception,
                "Request rejected. TraceId: {TraceId}, StatusCode: {StatusCode}",
                traceId, statusCode);
        }

        Dictionary<string, string[]> errors = [];
        errors.Add("General", [exception.Message]);

        ProblemDetails problemDetails = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}",
            Instance = httpContext.Request.Path,
            Detail = statusCode == 500 ? "An unexpected error occurred." : exception.Message,
        };

        problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Extensions["errors"] = errors;

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
