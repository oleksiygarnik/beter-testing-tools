using Confluent.Kafka;

namespace Beter.TestingTools.Consumer.Producers
{
    public interface IProducerMessageConverter<in T>
    {
        Message<string, byte[]> ConvertToKafkaMessage(T message);
    }
}
