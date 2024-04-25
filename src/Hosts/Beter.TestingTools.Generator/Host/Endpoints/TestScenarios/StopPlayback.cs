using Beter.TestingTool.Generator.Application.Contracts.TestScenarios;
using Beter.TestingTool.Generator.Contracts.Requests;
using Beter.TestingTool.Generator.Contracts.Responses;
using Beter.TestingTool.Generator.Host.Common.ApplicationConfiguration.Interfaces;
using Beter.TestingTool.Generator.Host.Common.Constants;
using System.Diagnostics.CodeAnalysis;

namespace Beter.TestingTool.Generator.Host.Endpoints.TestScenarios;

[ExcludeFromCodeCoverage]
public class StopPlayback : IEndpointProvider
{
    public static void DefineEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{ApiConstant.ApiPrefix}/test-scenarios/stop", StopPlaybackHandler)
            .WithName("StopPlayback")
            .Accepts<StopPlaybackRequest>(ApiConstant.ContentType)
            .Produces<StopPlaybackItemResponse>()
            .WithTags(ApiConstant.TestScenarioTag);
    }

    private static IResult StopPlaybackHandler(StopPlaybackRequest request, ITestScenarioService testScenarioService)
    {
        var response = testScenarioService.Stop(request);

        return Results.Ok(response);
    }
}
