using Beter.TestingTools.Generator.Infrastructure.Services.FeedConnections;
using Polly.Extensions.Http;
using Polly;
using System.Text;
using System.Text.Json;
using System.Net;
using Beter.TestingTools.IntegrationTests.Helpers;

namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public class BaseHttpClient
    {
        private readonly HttpClient _httpClient;

        public BaseHttpClient()
        {
            _httpClient = new HttpClient();
        }

        protected async Task<string> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var retryPolicy = GetRetryPolicy();
            var circuitBreakerPolicy = GetCircuitBreakerPolicy();

            var response = await retryPolicy
                .WrapAsync(circuitBreakerPolicy)
                .ExecuteAsync(() => _httpClient.SendAsync(request, cancellationToken));

            if (!response.IsSuccessStatusCode)
            {
                ThrowBadRequestException(request.RequestUri);
            }

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task WaitForServiceReadiness()
        {
            await _httpClient.GetAsyncAndWaitForStatusCode("/health", HttpStatusCode.OK);
        }

        protected static StringContent MapToContent<T>(T data) where T : class
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        private static void ThrowBadRequestException(Uri requestUri) =>
            throw new BadRequestException($"Unknown error occured during processing uri: {requestUri}.");

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() => 
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
