using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Common.Serialization;
using Beter.TestingTools.Generator.Application.Contracts.TestScenarios;
using Beter.TestingTools.Generator.Application.Extensions;
using Beter.TestingTools.Generator.Domain;
using Beter.TestingTools.Generator.Domain.TestScenarios;
using Beter.TestingTools.Models;
using Beter.TestingTools.Models.GlobalEvents;
using Beter.TestingTools.Models.Incidents;
using Beter.TestingTools.Models.Scoreboards;
using Beter.TestingTools.Models.TimeTableItems;
using Beter.TestingTools.Models.TradingInfos;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Beter.TestingTools.Generator.Scripts
{
    public sealed class TranformToFeedSerializationFormatScript : IExecutableScript
    {
        private readonly ITestScenarioFactory _factory;

        public TranformToFeedSerializationFormatScript(ITestScenarioFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task Run()
        {
            var testScenarios = _factory.Create(TestScenario.ResourcesPath);

            foreach (var testScenario in testScenarios)
            {
                var fileContent = TransformToNewFormat(testScenario);
                await WriteToFile(fileContent, testScenario.CaseId.ToString());
            }
        }

        private static async Task WriteToFile(string fileContent, string fileName)
        {
            var testScenarioFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.json");
            await File.WriteAllTextAsync(testScenarioFilePath, fileContent);
        }

        private string TransformToNewFormat(TestScenario testScenario)
        {
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
                                foreach (var outcome in market.Outcomes.Where(x => x.Prices != null))
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

            return JsonHubSerializer.Serialize(testScenario);
        }
    }
}
