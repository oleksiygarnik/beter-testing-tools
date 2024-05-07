﻿using Beter.TestingTools.Consumer.Domain;

namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IConsumerServiceHttpClient : IServiceReadinessChecker
    {
        Task LoadTestScenario(byte[] fileContent, CancellationToken cancellationToken = default);

        Task<TestScenarioTemplate> GetTemplate(CancellationToken cancellationToken = default);
    }
}
