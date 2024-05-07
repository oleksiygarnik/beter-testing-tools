using Beter.TestingTools.Consumer.Clients;
using Microsoft.Extensions.Logging;

namespace Beter.TestingTools.Consumer.Consumers;

internal class IncidentConsumer : ConsumerBase<IncidentFeedServiceClient>
{
    public IncidentConsumer(
        IncidentFeedServiceClient incidentFeedServiceClient,
        ILogger<IncidentConsumer> logger)
        : base(incidentFeedServiceClient, logger)
    {
    }
}