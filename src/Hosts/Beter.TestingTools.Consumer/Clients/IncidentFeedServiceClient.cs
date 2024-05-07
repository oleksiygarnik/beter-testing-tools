using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Consumer.Options;
using Beter.TestingTools.Consumer.Producers;
using Beter.TestingTools.Consumer.Services.Abstract;
using Beter.TestingTools.Models.Incidents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Beter.TestingTools.Consumer.Clients;

public class IncidentFeedServiceClient : FeedServiceClientBase<IncidentModel>
{
    public override string Channel => ChannelNames.Incident;

    public IncidentFeedServiceClient(
        ITestScenarioTemplateService templateService,
        IFeatureManager featureManager,
        ILogger<IncidentFeedServiceClient> logger,
        IOptions<FeedServiceOptions> feedServiceOptions,
        IFeedMessageProducer<IncidentModel> producer)
        : base(templateService, featureManager, logger, feedServiceOptions, producer)
    {
    }
}