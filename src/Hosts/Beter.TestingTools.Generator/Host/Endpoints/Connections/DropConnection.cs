﻿using Beter.TestingTool.Generator.Application.Contracts.FeedConnections;
using Beter.TestingTool.Generator.Host.Common.ApplicationConfiguration.Interfaces;
using Beter.TestingTool.Generator.Host.Common.Constants;
using System.Diagnostics.CodeAnalysis;

namespace Beter.TestingTool.Generator.Host.Endpoints.Connections;

[ExcludeFromCodeCoverage]
public class DropConnection : IEndpointProvider
{
    public static void DefineEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete($"{ApiConstant.ApiPrefix}/connections/{{id}}/", DropСonnectionHandler)
            .WithName("DropСonnection")
            .WithTags(ApiConstant.ConnectionTag);
    }

    private static async Task<IResult> DropСonnectionHandler(HttpContext context, string id, IFeedConnectionService feedConnectionService, CancellationToken cancellationToken = default)
    {
        await feedConnectionService.DropConnectionAsync(id, cancellationToken);

        return Results.NoContent();
    }
}
