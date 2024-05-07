namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IGeneratorServiceUrlProvider
    {
        Uri BaseUrl();

        Uri LoadTestScenario();

        Uri RunTestScenario();
    }
}
