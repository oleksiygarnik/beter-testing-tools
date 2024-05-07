using Confluent.Kafka;

namespace Beter.TestingTools.Consumer.Producers
{
    public interface IProducerFactory
    {
        public IProducer<TKey, TValue> Create<TKey, TValue>(PublishOptions config);
    }
}
