namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IConsumerServiceUrlProvider
    {
        Uri BaseUrl();

        Uri LoadTestScenario();

        Uri GetTemplate();
    }
}
