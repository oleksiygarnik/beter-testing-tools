﻿using Confluent.Kafka;

namespace Beter.TestingTools.Consumer.Producers
{
    public class PublishOptions
    {
        public const string SectionName = "Publisher";
        public string BootstrapServers { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public string SslKeyLocation { get; set; } = null!;
        public string SslCertificateLocation { get; set; } = null!;
        public bool AllowAutoCreateTopics { get; set; } = false;
        public SecurityProtocol SecurityProtocol { get; set; } = SecurityProtocol.Plaintext;
    }
}
