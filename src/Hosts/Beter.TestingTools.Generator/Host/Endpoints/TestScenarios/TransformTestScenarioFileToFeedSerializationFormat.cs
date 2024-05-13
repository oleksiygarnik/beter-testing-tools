using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Common.Serialization;
using Beter.TestingTools.Generator.Application.Extensions;
using Beter.TestingTools.Generator.Domain;
using Beter.TestingTools.Generator.Domain.TestScenarios;
using Beter.TestingTools.Generator.Host.Common.ApplicationConfiguration.Interfaces;
using Beter.TestingTools.Generator.Host.Common.Constants;
using Beter.TestingTools.Models;
using Beter.TestingTools.Models.GlobalEvents;
using Beter.TestingTools.Models.Incidents;
using Beter.TestingTools.Models.Scoreboards;
using Beter.TestingTools.Models.TimeTableItems;
using Beter.TestingTools.Models.TradingInfos;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Beter.TestingTools.Generator.Host.Endpoints.TestScenarios
{
    /// <summary>
    /// This endpoint serves a temporary purpose and should be reviewed for potential removal in the near future.
    /// </summary>
    public sealed class TransformTestScenarioFileToFeedSerializationFormat : IEndpointProvider
    {
        public static void DefineEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost($"{ApiConstant.ApiPrefix}/test-scenarios/transform", TransformTestScenarioFileToFeedSerializationFormatHandler)
                .WithName("MapTestScenarioFileToFeedSerializationFormat")
                .WithTags(ApiConstant.TestScenarioTag);
        }

        private async static Task<IResult> TransformTestScenarioFileToFeedSerializationFormatHandler(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("File is empty.");

            using (var stream = file.OpenReadStream())
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                if (!int.TryParse(fileName, out var caseId))
                    return Results.BadRequest("Invalid file name format.");

                using (var streamReader = new StreamReader(stream))
                {
                    var content = await streamReader.ReadToEndAsync();
                    var testScenario = JsonHubSerializer.Deserialize<TestScenario>(content);

                    foreach (var message in testScenario.Messages)
                    {
                        dynamic model;
                        switch (message.MessageType)
                        {
                            case MessageTypes.SteeringCommand:
                                model = message.Value.Deserialize<SteeringCommand>();
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(model));
                                break;
                            case MessageTypes.SubscriptionsRemoved:
                                model = message.Value.Deserialize<SubscriptionsRemovedModel>();
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(model));
                                break;
                            case MessageTypes.SystemEvent:
                                model = message.Value.Deserialize<IEnumerable<GlobalMessageModel>>();
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(model));
                                break;
                            case MessageTypes.Incident:
                                model = message.Value.Deserialize<IEnumerable<IncidentModel>>();
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(model));
                                break;
                            case MessageTypes.Scoreboard:
                                model = message.Value.Deserialize<IEnumerable<ScoreBoardModel>>();
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(model));
                                break;
                            case MessageTypes.Trading:
                                var tradingInfo = message.Value.Deserialize<IEnumerable<TradingInfoModel>>();

                                foreach (var info in tradingInfo)
                                {
                                    foreach (var market in info.Markets)
                                    {
                                        foreach (var outcome in market.Outcomes)
                                        {
                                            var availableKeys = outcome.Prices.Keys;

                                            outcome.Prices = outcome.Price.ToAllFormats().Where(x => availableKeys.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                                        }
                                    }
                                }
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(tradingInfo));
                                break;
                            case MessageTypes.Timetable:
                                model = message.Value.Deserialize<IEnumerable<TimeTableItemModel>>();
                                message.Value = JsonNode.Parse(JsonHubSerializer.Serialize(model));
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                    }

                    var fileContent = Encoding.UTF8.GetBytes(JsonHubSerializer.Serialize(testScenario));

                    return Results.File(
                        fileContent,
                        "application/json",
                        caseId.ToString());
                }
            }
        }
    }
}
