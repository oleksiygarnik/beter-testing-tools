using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Consumer.Options;
using Beter.TestingTools.Consumer.Producers;
using Beter.TestingTools.Consumer.Services.Abstract;
using Beter.TestingTools.Models.TradingInfos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Beter.TestingTools.Consumer.Clients;

public class TradingFeedServiceClient : FeedServiceClientBase<TradingInfoModel>
{
    public override string Channel => ChannelNames.Trading;

    public TradingFeedServiceClient(
        ITestScenarioTemplateService templateService,
        IFeatureManager featureManager,
        ILogger<FeedServiceClientBase<TradingInfoModel>> logger,
        IOptions<FeedServiceOptions> feedServiceOptions,
        IFeedMessageProducer<TradingInfoModel> producer)
        : base(templateService, featureManager, logger, feedServiceOptions, producer)
    {
    }
}