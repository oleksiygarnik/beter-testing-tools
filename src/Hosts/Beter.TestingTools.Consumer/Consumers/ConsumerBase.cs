using Beter.TestingTools.Consumer.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Beter.TestingTools.Consumer.Consumers;

internal abstract class ConsumerBase<T> : IHostedService
    where T : IFeedClient
{
    private readonly ILogger _logger;
    private readonly T _feedServiceClient;

    public ConsumerBase(T feedServiceClient, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(feedServiceClient);
        ArgumentNullException.ThrowIfNull(logger);

        _feedServiceClient = feedServiceClient;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{typeof(T).Name} is running.");
        await _feedServiceClient.ConnectAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(T)} is stopping.");
        await _feedServiceClient.DisconnectAsync(cancellationToken);
    }
}
