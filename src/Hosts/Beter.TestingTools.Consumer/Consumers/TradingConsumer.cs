using Beter.TestingTools.Consumer.Clients;
using Microsoft.Extensions.Logging;

namespace Beter.TestingTools.Consumer.Consumers;

internal class TradingConsumer : ConsumerBase<TradingFeedServiceClient>
{
    public TradingConsumer(
        TradingFeedServiceClient tradingFeedServiceClient,
        ILogger<TradingConsumer> logger)
        : base(tradingFeedServiceClient, logger)
    {
    }
}