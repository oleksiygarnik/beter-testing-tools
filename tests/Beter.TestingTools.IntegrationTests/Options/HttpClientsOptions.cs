﻿namespace Beter.TestingTools.IntegrationTests.Options
{
    public class HttpClientsOptions
    {
        public const string Section = "HttpClients";

        public string GeneratorServiceHost { get; set; }
        public string ConsumerServiceHost { get; set; }
    }
}