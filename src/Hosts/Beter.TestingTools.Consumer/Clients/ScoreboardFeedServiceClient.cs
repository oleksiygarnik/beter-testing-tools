using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Consumer.Options;
using Beter.TestingTools.Consumer.Producers;
using Beter.TestingTools.Consumer.Services.Abstract;
using Beter.TestingTools.Models.Scoreboards;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Beter.TestingTools.Consumer.Clients;

public class ScoreboardFeedServiceClient : FeedServiceClientBase<ScoreBoardModel>
{
    public override string Channel => ChannelNames.Scoreboard;

    public ScoreboardFeedServiceClient(
        ITestScenarioTemplateService templateService,
        IFeatureManager featureManager,
        ILogger<FeedServiceClientBase<ScoreBoardModel>> logger,
        IOptions<FeedServiceOptions> feedServiceOptions,
        IFeedMessageProducer<ScoreBoardModel> producer)
        : base(templateService, featureManager, logger, feedServiceOptions, producer)
    {
    }
}