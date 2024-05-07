using Beter.TestingTools.Models;
using Beter.TestingTools.Models.GlobalEvents;

namespace Beter.TestingTools.Consumer.Producers;

public interface IFeedMessageProducer<TMessage> where TMessage : class, IIdentityModel
{
    Task ProduceAsync(IEnumerable<TMessage> messages, string channel, CancellationToken cancellationToken = default);
    Task ProduceAsync(IEnumerable<GlobalMessageModel> messages, string channel, CancellationToken cancellationToken = default);
    Task ProduceAsync(SubscriptionsRemovedModel message, string channel, CancellationToken cancellationToken = default);
}

