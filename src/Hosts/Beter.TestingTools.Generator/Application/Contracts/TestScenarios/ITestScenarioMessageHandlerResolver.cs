﻿namespace Beter.TestingTool.Generator.Application.Contracts.TestScenarios;

public interface ITestScenarioMessageHandlerResolver
{
    ITestScenarioMessageHandler Resolve(string messageType);
}

