﻿using Beter.TestingTools.Consumer.Clients;
using Beter.TestingTools.Consumer.Consumers;
using Beter.TestingTools.Consumer.Models;
using Beter.TestingTools.Consumer.Options;
using Beter.TestingTools.Consumer.Producers;
using Beter.TestingTools.Consumer.Services;
using Beter.TestingTools.Consumer.Services.Abstract;
using Beter.TestingTools.Models;
using Beter.TestingTools.Models.GlobalEvents;
using Beter.TestingTools.Models.Incidents;
using Beter.TestingTools.Models.Scoreboards;
using Beter.TestingTools.Models.TimeTableItems;
using Beter.TestingTools.Models.TradingInfos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;

namespace Beter.TestingTools.Consumer.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement(configuration.GetSection(FeatureManagementFlags.Section));

        services.AddSingleton<ITestScenarioTemplateService, TestScenarioTemplateService>();
        services.AddSingleton<ITestScenarioFactory, TestScenarioFactory>();
    }

    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<TradingConsumer>();
        services.AddHostedService<TimeTableConsumer>();
        services.AddHostedService<ScoreboardConsumer>();
        services.AddHostedService<IncidentConsumer>();
    }

    public static void AddFeedServiceClients(this IServiceCollection services)
    {
        services.AddSingleton<TradingFeedServiceClient>();
        services.AddSingleton<TimeTableFeedServiceClient>();
        services.AddSingleton<ScoreboardFeedServiceClient>();
        services.AddSingleton<IncidentFeedServiceClient>();
    }

    public static IServiceCollection AddKafkaConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FeedServiceOptions>(configuration.GetSection(FeedServiceOptions.SectionName));
        services.Configure<PublishOptions>(configuration.GetSection(PublishOptions.SectionName));
        services.AddSingleton<IProducerFactory, ProducerFactory>();

        return services;
    }

    public static void AddFeedMessageProducers(this IServiceCollection services)
    {
        services.AddKafkaMessageProducer<FeedMessageModel<IEnumerable<TimeTableItemModel>>, FeedMessageConverter<IEnumerable<TimeTableItemModel>>>();
        services.AddKafkaMessageProducer<FeedMessageModel<IEnumerable<ScoreBoardModel>>, FeedMessageConverter<IEnumerable<ScoreBoardModel>>>();
        services.AddKafkaMessageProducer<FeedMessageModel<IEnumerable<TradingInfoModel>>, FeedMessageConverter<IEnumerable<TradingInfoModel>>>();
        services.AddKafkaMessageProducer<FeedMessageModel<IEnumerable<IncidentModel>>, FeedMessageConverter<IEnumerable<IncidentModel>>>();
        services.AddKafkaMessageProducer<FeedMessageModel<IEnumerable<GlobalMessageModel>>, FeedMessageConverter<IEnumerable<GlobalMessageModel>>>();
        services.AddKafkaMessageProducer<FeedMessageModel<SubscriptionsRemovedModel>, FeedMessageConverter<SubscriptionsRemovedModel>>();

        services.AddSingleton<IFeedMessageProducer<TimeTableItemModel>, FeedMessageProducer<TimeTableItemModel>>();
        services.AddSingleton<IFeedMessageProducer<ScoreBoardModel>, FeedMessageProducer<ScoreBoardModel>>();
        services.AddSingleton<IFeedMessageProducer<TradingInfoModel>, FeedMessageProducer<TradingInfoModel>>();
        services.AddSingleton<IFeedMessageProducer<IncidentModel>, FeedMessageProducer<IncidentModel>>();
    }

    public static IServiceCollection AddKafkaMessageProducer<T, TMessageConverter>(this IServiceCollection serviceCollection)
       where TMessageConverter : class, IProducerMessageConverter<T>
    {
        serviceCollection.TryAddSingleton<IProducerMessageConverter<T>, TMessageConverter>();
        serviceCollection.TryAddSingleton<IProducerTransport<T>, ProducerTransport<T>>();
        return serviceCollection;
    }
}