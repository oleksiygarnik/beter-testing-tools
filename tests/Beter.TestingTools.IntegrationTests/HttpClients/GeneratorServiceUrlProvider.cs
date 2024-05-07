using Beter.TestingTools.IntegrationTests.HttpClients.Abstract;
using Beter.TestingTools.IntegrationTests.Options;
using Microsoft.Extensions.Options;

namespace Beter.TestingTools.IntegrationTests.HttpClients
{
    public sealed class GeneratorServiceUrlProvider : IGeneratorServiceUrlProvider
    {
        private readonly Uri _baseUrl;

        public GeneratorServiceUrlProvider(IOptions<HttpClientsOptions> httpClientsOptions)
        {
            _baseUrl = new Uri(httpClientsOptions.Value.GeneratorServiceHost);
        }

        public Uri BaseUrl() => _baseUrl;
        public Uri LoadTestScenario() => new Uri(_baseUrl, "/api/test-scenarios/load");
        public Uri RunTestScenario() => new Uri(_baseUrl, "/api/test-scenarios/run");
    }
}
