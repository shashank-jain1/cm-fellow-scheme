using CmScheme.HelpDesk.Application.Features.Sla.CheckSlaBreaches;
using Mediator;

namespace CmScheme.Api.BackgroundServices;

public sealed class SlaCheckBackgroundService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<SlaCheckBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("SLA Check Background Service started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using IServiceScope scope = serviceScopeFactory.CreateScope();
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                logger.LogInformation("Executing background SLA breach check...");
                var result = await mediator.Send(new CheckSlaBreachesCommand(), stoppingToken);

                if (result.IsSuccess)
                {
                    logger.LogInformation("SLA breach check completed successfully.");
                }
                else
                {
                    logger.LogWarning("SLA breach check completed with warnings/errors.");
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during background SLA breach check execution.");
            }

            try
            {
                // Run check every 15 minutes
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }

        logger.LogInformation("SLA Check Background Service stopped.");
    }
}
