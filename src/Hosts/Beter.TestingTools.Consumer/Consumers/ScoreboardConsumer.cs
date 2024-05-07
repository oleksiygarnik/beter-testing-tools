using Beter.TestingTools.Consumer.Clients;
using Microsoft.Extensions.Logging;

namespace Beter.TestingTools.Consumer.Consumers;

internal class ScoreboardConsumer : ConsumerBase<ScoreboardFeedServiceClient>
{
    public ScoreboardConsumer(
        ScoreboardFeedServiceClient scoreboardFeedServiceClient,
        ILogger<ScoreboardConsumer> logger)
        : base(scoreboardFeedServiceClient, logger)
    {
    }
}