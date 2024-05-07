using Beter.TestingTools.Consumer.Clients;
using Microsoft.Extensions.Logging;

namespace Beter.TestingTools.Consumer.Consumers;

internal class TimeTableConsumer : ConsumerBase<TimeTableFeedServiceClient>
{
    public TimeTableConsumer(
        TimeTableFeedServiceClient timeTableFeedServiceClient,
        ILogger<TimeTableConsumer> logger)
        : base(timeTableFeedServiceClient, logger)
    {
    }
}
