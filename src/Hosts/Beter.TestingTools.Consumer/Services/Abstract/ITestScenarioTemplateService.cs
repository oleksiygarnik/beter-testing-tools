using Beter.TestingTools.Common.Enums;
using Beter.TestingTools.Consumer.Domain;
using System.Text.Json.Nodes;

namespace Beter.TestingTools.Consumer.Services.Abstract
{
    public interface ITestScenarioTemplateService
    {
        JsonNode GetNext(HubKind hubKind);
        TestScenarioTemplate GetTemplate();
        TestScenarioTemplate SetTemplate(TestScenario testScenario);
        TestScenarioTemplate SetMissmatchItem(HubKind hubKind, string expected, string actual);
    }
}
