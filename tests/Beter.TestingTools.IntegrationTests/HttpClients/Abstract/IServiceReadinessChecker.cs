namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IServiceReadinessChecker
    {
        Task WaitForServiceReadiness();
    }
}
