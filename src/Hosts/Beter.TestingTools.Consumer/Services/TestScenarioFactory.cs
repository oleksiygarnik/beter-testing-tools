using Beter.TestingTools.Consumer.Domain;
using Beter.TestingTools.Consumer.Services.Abstract;
using System.Text.Json;

namespace Beter.TestingTools.Consumer.Services
{
    public sealed class TestScenarioFactory : ITestScenarioFactory
    {
        public async Task<TestScenario> Create(int caseId, Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync();

            try
            {
                return JsonSerializer.Deserialize<TestScenario>(content);
            }
            catch (JsonException e)
            {
                throw new ArgumentException("The content of the test scenario file is invalid JSON.", e);
            }
        }
    }
}
