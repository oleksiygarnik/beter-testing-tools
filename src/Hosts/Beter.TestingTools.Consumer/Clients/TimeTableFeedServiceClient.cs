using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Consumer.Options;
using Beter.TestingTools.Consumer.Producers;
using Beter.TestingTools.Consumer.Services.Abstract;
using Beter.TestingTools.Models.TimeTableItems;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Beter.TestingTools.Consumer.Clients;

public class TimeTableFeedServiceClient : FeedServiceClientBase<TimeTableItemModel>
{
    public override string Channel => ChannelNames.Timetable;

    public TimeTableFeedServiceClient(
        ITestScenarioTemplateService templateService,
        IFeatureManager featureManager,
        ILogger<FeedServiceClientBase<TimeTableItemModel>> logger,
        IOptions<FeedServiceOptions> feedServiceOptions,
        IFeedMessageProducer<TimeTableItemModel> producer)
        : base(templateService, featureManager, logger, feedServiceOptions, producer)
    {
    }
}