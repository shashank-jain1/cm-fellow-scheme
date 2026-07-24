using System.Reflection;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Mediator;

namespace CmScheme.Common.Core.Behaviours;

public sealed class CommandValidationBehaviour<TCommand, TResponse>
    : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class, IResult
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public CommandValidationBehaviour(IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators ?? [];
    }

    public async ValueTask<TResponse> Handle(
        TCommand command,
        MessageHandlerDelegate<TCommand, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(command, cancellationToken);

        ValidationContext<TCommand> context = new(command);
        ValidationError[] errors = _validators
            .Select(x => x.Validate(context))
            .Where(x => !x.IsValid)
            .SelectMany(x => x.AsErrors())
            .ToArray();

        if (errors.Length == 0)
            return await next(command, cancellationToken);

        TResponse? response = typeof(TResponse)
            .GetMethod(nameof(Result.Invalid),
                BindingFlags.Static | BindingFlags.Public,
                [typeof(IEnumerable<ValidationError>)])?
            .Invoke(null, [errors]) as TResponse
            ?? Result.Invalid(errors) as TResponse;

        return response ?? throw new ValidationException("Validation errors.");
    }
}
