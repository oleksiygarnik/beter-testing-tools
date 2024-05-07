using Beter.TestingTools.Generator.Contracts.Requests;
using Beter.TestingTools.Generator.Domain.Playbacks;

namespace Beter.TestingTools.IntegrationTests.HttpClients.Abstract
{
    public interface IGeneratorServiceHttpClient
    {
        Task LoadTestScenario(byte[] fileContent, CancellationToken cancellationToken = default);

        Task<Playback> RunTestScenario(StartPlaybackRequest request, CancellationToken cancellationToken = default);
    }
}
