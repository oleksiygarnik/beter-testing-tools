﻿using Beter.TestingTools.Common.Enums;
using System.Text.Json.Nodes;

namespace Beter.TestingTools.Consumer.Domain
{
    public sealed class TestScenarioTemplate
    {
        public Dictionary<HubKind, Queue<JsonNode>> Messages { get; set; } = new();
        public Dictionary<HubKind, List<TestScenarioTemplateMissmatchItem>> MissmatchItems { get; set; } = new();

        public bool IsProcessed => !Messages.Any();
        public bool IsFailed => MissmatchItems.Any();
    }
}