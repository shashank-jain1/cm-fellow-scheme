using System.Diagnostics;
using Ardalis.Result;
using Mediator;
using Microsoft.Extensions.Logging;

namespace CmScheme.Common.Core.Behaviours;

public sealed class ElapsedTimeBehaviour<TMessage, TResponse>
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : class, IResult
{
    private readonly ILogger<ElapsedTimeBehaviour<TMessage, TResponse>> _logger;

    public ElapsedTimeBehaviour(ILogger<ElapsedTimeBehaviour<TMessage, TResponse>> logger)
    {
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        try
        {
            TResponse result = await next(message, cancellationToken);
            stopwatch.Stop();
            _logger.LogInformation("Elapsed time for {FullName}: {ElapsedMilliseconds} ms",
                typeof(TMessage).FullName, stopwatch.ElapsedMilliseconds);
            return result;
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            _logger.LogError(e, "Error while executing {FullName}. Elapsed: {ElapsedMilliseconds} ms",
                typeof(TMessage).FullName, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
