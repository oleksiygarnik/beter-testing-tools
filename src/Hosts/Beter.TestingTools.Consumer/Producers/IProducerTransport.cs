using Confluent.Kafka;

namespace Beter.TestingTools.Consumer.Producers
{
    public interface IProducerTransport<T>
    {
        Task<DeliveryResult<string, byte[]>> ProduceAsync(string topic, T message, CancellationToken cancellationToken = default);
    }
}
