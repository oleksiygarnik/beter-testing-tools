using Beter.TestingTools.IntegrationTests.HttpClients;
using Beter.TestingTools.IntegrationTests.HttpClients.Abstract;
using Beter.TestingTools.IntegrationTests.Infrastructure;
using Beter.TestingTools.IntegrationTests.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Beter.TestingTools.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGeneratorServiceUrlProvider, GeneratorServiceUrlProvider>();
            services.AddSingleton<IGeneratorServiceHttpClient, GeneratorServiceHttpClient>();

            services.AddSingleton<IConsumerServiceUrlProvider, ConsumerServiceUrlProvider>();
            services.AddSingleton<IConsumerServiceHttpClient, ConsumerServiceHttpClient>();

            services.Configure<HttpClientsOptions>(TestConfiguration.Get.GetSection(HttpClientsOptions.Section));
        }
    }
}
