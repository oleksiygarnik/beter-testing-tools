using Beter.TestingTools.Consumer.Domain;

namespace Beter.TestingTools.Consumer.Services.Abstract
{
    public interface ITestScenarioFactory
    {
        Task<TestScenario> Create(int caseId, Stream stream);
    }
}
