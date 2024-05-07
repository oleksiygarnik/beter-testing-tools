using Beter.TestingTools.Generator.Contracts.Requests;
using Beter.TestingTools.IntegrationTests.Helpers;
using Beter.TestingTools.IntegrationTests.HttpClients.Abstract;
using Beter.TestingTools.IntegrationTests.Infrastructure;
using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Xunit.Abstractions;

namespace Beter.TestingTools.IntegrationTests.Tests
{
    public class End2EndTests : DockerComposeTestBase
    {
        private readonly ITestOutputHelper _output;
        private readonly IConsumerServiceHttpClient _consumerHttpClient;
        private readonly IGeneratorServiceHttpClient _generatorHttpClient;

        public End2EndTests(IConsumerServiceHttpClient consumerHttpClient, IGeneratorServiceHttpClient generatorHttpClient, ITestOutputHelper output)
        {
            _output = output;
            _consumerHttpClient = consumerHttpClient ?? throw new ArgumentNullException(nameof(consumerHttpClient));
            _generatorHttpClient = generatorHttpClient ?? throw new ArgumentNullException(nameof(generatorHttpClient));
        }

        protected override ICompositeService Build()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), (TemplateString)"docker-compose.tests.yml");

            return new DockerComposeCompositeService(
                DockerHost,
                new DockerComposeConfig
                {
                    ComposeFilePath = new List<string> { file },
                    ForceBuild = true,
                    ForceRecreate = true,
                    RemoveOrphans = true,
                    StopOnDispose = true
                });
        }

        [Fact]
        public async Task Generate_And_Emulate_Data_And_Consume_This_Data()
        {
            //Arrange
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/1.json");
            var fileContent = File.ReadAllBytes(directoryPath);

            _output.WriteLine("Send load test scenario to Generator.");
            await _generatorHttpClient.LoadTestScenario(fileContent, CancellationToken.None);

            await Task.Delay(10000);
            _output.WriteLine("Send load test scenario to Consumer.");
            await _consumerHttpClient.LoadTestScenario(fileContent, CancellationToken.None);

            var request = new StartPlaybackRequest()
            {
                CaseId = 1,
                TimeOffsetAfterFirstTimetableMessageInSecounds = 0,
                TimeOffsetInMinutes = 0
            };

            //Act
            _output.WriteLine("Run test scenario in Generator.");
            await _generatorHttpClient.RunTestScenario(request, CancellationToken.None);

            //Assert
            await WaitHelper.WaitForCondition(async () =>
            {
                var response = await _consumerHttpClient.GetTemplate();

                return response.IsProcessed && !response.IsFailed;
            });
        }
    }

    public class MakeConsoleWork : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly TextWriter _originalOut;
        private readonly TextWriter _textWriter;

        public MakeConsoleWork(ITestOutputHelper output)
        {
            _output = output;
            _originalOut = Console.Out;
            _textWriter = new StringWriter();
            Console.SetOut(_textWriter);
        }

        public void Dispose()
        {
            _output.WriteLine(_textWriter.ToString());
            Console.SetOut(_originalOut);
        }
    }
}
