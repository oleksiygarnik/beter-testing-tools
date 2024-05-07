using Beter.TestingTools.Consumer.Domain;
using Beter.TestingTools.IntegrationTests.HttpClients.Abstract;
using System.Text.Json;

namespace Beter.TestingTools.IntegrationTests.HttpClients
{
    public sealed class ConsumerServiceHttpClient : BaseHttpClient, IConsumerServiceHttpClient
    {
        private readonly IConsumerServiceUrlProvider _urlProvider;

        public ConsumerServiceHttpClient(IConsumerServiceUrlProvider urlProvider) : base(urlProvider?.BaseUrl())
        {
            _urlProvider = urlProvider ?? throw new ArgumentNullException(nameof(urlProvider));
        }

        public async Task<TestScenarioTemplate> GetTemplate(CancellationToken cancellationToken = default)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = _urlProvider.GetTemplate()
            };

            var response = await SendRequest(requestMessage, cancellationToken);

            return JsonSerializer.Deserialize<TestScenarioTemplate>(response);
        }

        public async Task LoadTestScenario(byte[] fileContent, CancellationToken cancellationToken = default)
        {
            if (fileContent is null)
                throw new ArgumentNullException(nameof(fileContent));

            var byteArrayContent = new ByteArrayContent(fileContent);
            var multipartContent = new MultipartFormDataContent
            {
                { byteArrayContent, "file", "1" }
            };

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _urlProvider.LoadTestScenario(),
                Content = multipartContent
            };

            await SendRequest(requestMessage, cancellationToken);
        }
    }
}
