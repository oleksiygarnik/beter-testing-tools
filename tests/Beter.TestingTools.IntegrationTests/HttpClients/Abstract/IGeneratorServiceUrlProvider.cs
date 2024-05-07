namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IGeneratorServiceUrlProvider
    {
        Uri LoadTestScenario();

        Uri RunTestScenario();
    }
}
