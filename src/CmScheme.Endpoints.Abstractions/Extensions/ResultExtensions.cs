using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using CmScheme.Endpoints.Abstractions.ApiErrors;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Endpoints.Abstractions.Extensions;

public static class ResultExtensions
{
    public static IResult ToApiResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => TypedResults.Ok(result.Value),
            ResultStatus.Invalid => ValidationProblem(result),
            ResultStatus.Conflict => Conflict(result),
            ResultStatus.NotFound => NotFound(result),
            ResultStatus.CriticalError => InternalServerError(result),
            ResultStatus.Error => InternalServerError(result),
            ResultStatus.NoContent => TypedResults.NoContent(),
            ResultStatus.Unauthorized => Unauthorized(result),
            _ => TypedResults.Empty
        };
    }

    public static IResult ToApiResult(this Result result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => TypedResults.NoContent(),
            ResultStatus.Invalid => ValidationProblem(result),
            ResultStatus.Conflict => Conflict(result),
            ResultStatus.NotFound => NotFound(result),
            ResultStatus.CriticalError => InternalServerError(result),
            ResultStatus.Error => InternalServerError(result),
            ResultStatus.NoContent => TypedResults.NoContent(),
            ResultStatus.Unauthorized => Unauthorized(result),
            _ => TypedResults.Empty
        };
    }

    public static IResult ToApiResult<T>(this Result<T> result, string contentType, string fileName)
    {
        if (!result.IsSuccess) return result.ToApiResult();
        return result.Value switch
        {
            byte[] bytes => TypedResults.Bytes(bytes, contentType, fileName),
            ReadOnlyMemory<byte> memory => TypedResults.Bytes(memory, contentType, fileName),
            _ => result.ToApiResult()
        };
    }

    private static IResult ValidationProblem<T>(Result<T> result)
    {
        IEnumerable<ValidationError> validationErrors = result.ValidationErrors;
        return TypedResults.ValidationProblem(
            validationErrors.ToDictionary(
                e => e.Identifier,
                e => new[] { e.ErrorMessage }),
            title: "Validation errors.");
    }

    private static IResult ValidationProblem(Result result)
    {
        IEnumerable<ValidationError> validationErrors = result.ValidationErrors;
        return TypedResults.ValidationProblem(
            validationErrors.ToDictionary(
                e => e.Identifier,
                e => new[] { e.ErrorMessage }),
            title: "Validation errors.");
    }

    private static IResult Conflict<T>(Result<T> result)
    {
        return TypedResults.Conflict(new ApiProblemDetails(
            ErrorCode.Conflict,
            System.Net.HttpStatusCode.Conflict,
            "Conflict",
            string.Join(", ", result.Errors)));
    }

    private static IResult Conflict(Result result)
    {
        return TypedResults.Conflict(new ApiProblemDetails(
            ErrorCode.Conflict,
            System.Net.HttpStatusCode.Conflict,
            "Conflict",
            string.Join(", ", result.Errors)));
    }

    private static IResult NotFound<T>(Result<T> result)
    {
        return TypedResults.NotFound(new ApiProblemDetails(
            ErrorCode.NotFound,
            System.Net.HttpStatusCode.NotFound,
            "Not Found",
            string.Join(", ", result.Errors)));
    }

    private static IResult NotFound(Result result)
    {
        return TypedResults.NotFound(new ApiProblemDetails(
            ErrorCode.NotFound,
            System.Net.HttpStatusCode.NotFound,
            "Not Found",
            string.Join(", ", result.Errors)));
    }

    private static IResult InternalServerError<T>(Result<T> result)
    {
        return TypedResults.Json(new ApiProblemDetails(
            ErrorCode.ServerError,
            System.Net.HttpStatusCode.InternalServerError,
            "Server Error",
            string.Join(", ", result.Errors)),
            statusCode: 500);
    }

    private static IResult InternalServerError(Result result)
    {
        return TypedResults.Json(new ApiProblemDetails(
            ErrorCode.ServerError,
            System.Net.HttpStatusCode.InternalServerError,
            "Server Error",
            string.Join(", ", result.Errors)),
            statusCode: 500);
    }

    private static IResult Unauthorized<T>(Result<T> result)
    {
        return TypedResults.Json(new ApiProblemDetails(
            ErrorCode.Unauthorized,
            System.Net.HttpStatusCode.Unauthorized,
            "Unauthorized",
            string.Join(", ", result.Errors)),
            statusCode: 401);
    }

    public static async ValueTask<IResult> ToApiResultAsync<T>(this ValueTask<Result<T>> result)
        => (await result.ConfigureAwait(false)).ToApiResult();

    public static async ValueTask<IResult> ToApiResultAsync(this ValueTask<Result> result)
        => (await result.ConfigureAwait(false)).ToApiResult();
}
