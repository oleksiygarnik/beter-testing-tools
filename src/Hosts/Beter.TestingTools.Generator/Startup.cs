using Beter.TestingTools.Hosting;
using Beter.TestingTools.Generator.Host.Middlewares;
using Beter.TestingTools.Generator.Infrastructure.Extensions;
using Beter.TestingTools.Generator.Host.Common.ApplicationConfiguration.Extensions;
using Beter.TestingTools.Generator.Host.Extensions;
using Beter.TestingTools.Generator.Application.Extensions;
using Beter.B2B.Generator;

namespace Beter.TestingTools.Generator;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddGeneratorSwagger()
            .AddInfrastructure(Configuration)
            .AddApplicationServices(Configuration)
            .AddFeedConnections(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseRouting();
        app.UseGeneratorSwagger();
        app.UseEndpoints<Program>();
        app.AddHealthCheckEndpoint();
        app.ConfigureMaxRequestBodySize();
    }
}