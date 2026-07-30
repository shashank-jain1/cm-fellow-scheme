using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CmScheme.Api.Middleware;

public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        T? body = context.Arguments
            .OfType<T>()
            .FirstOrDefault();

        if (body is null)
        {
            return await next(context);
        }

        IValidator<T>? validator = _serviceProvider
            .GetService(typeof(IValidator<T>)) as IValidator<T>;

        if (validator is null)
        {
            return await next(context);
        }

        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(body);

        if (validationResult.IsValid)
        {
            return await next(context);
        }

        Dictionary<string, string[]> errors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        ValidationProblemDetails problemDetails = new ValidationProblemDetails(errors)
        {
            Title = "Validation Failed",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://httpstatuses.com/400",
        };

        return Results.Json(problemDetails, statusCode: StatusCodes.Status400BadRequest);
    }
}
