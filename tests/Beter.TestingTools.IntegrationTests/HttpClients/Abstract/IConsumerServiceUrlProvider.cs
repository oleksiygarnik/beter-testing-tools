namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IConsumerServiceUrlProvider
    {
        Uri LoadTestScenario();

        Uri GetTemplate();
    }
}
