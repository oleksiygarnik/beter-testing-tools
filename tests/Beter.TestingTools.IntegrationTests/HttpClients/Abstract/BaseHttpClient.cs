﻿using Beter.TestingTools.Generator.Infrastructure.Services.FeedConnections;
using System.Text;
using System.Text.Json;

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
            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                ThrowBadRequestException(request.RequestUri);
            }

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        protected static StringContent MapToContent<T>(T data) where T : class
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        private static void ThrowBadRequestException(Uri requestUri) =>
            throw new BadRequestException($"Unknown error occured during processing uri: {requestUri}.");
    }
}
