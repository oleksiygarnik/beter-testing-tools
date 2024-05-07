using Beter.B2B.Consumer;
using Beter.TestingTools.Consumer.Extensions;
using Beter.TestingTools.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Beter.TestingTools.Consumer;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddKafkaConfiguration(Configuration);
        services.AddHostedServices();
        services.AddEndpointsApiExplorer();
        services.AddFeedConsumerSwagger();
        services.AddFeedServiceClients();
        services.AddFeedMessageProducers();
        services.AddApplicationServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseFeedConsumerSwagger();
        app.AddHealthCheckEndpoint();
        app.UseEndpoints<Program>();
    }
}