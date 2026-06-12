using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Uber.Shared;
using Uber.Voyage.Domain.Repositories;
using Uber.Voyage.Infrastructure.Persistence.Repositories;

namespace Uber.Voyage.Infrastructure.Kafka.Consumers;

public sealed class VoyageConsumer(ILogger<VoyageConsumer> _logger, IConfiguration _configuration , IServiceScopeFactory _scopeFactory) : BackgroundService
{
    private static readonly string[] SagaTopics =
[
        KafkaTopics.VoyageRequested,
    ];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"],
            GroupId = "voyage-saga-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            //Debug = "consumer,cgrp,topic,fetch"
        };

        var consumer = new ConsumerBuilder<string, string>(config)
        .SetErrorHandler((_, e) => _logger.LogError("[Kafka Error] {Reason}", e.Reason))
         //.SetLogHandler((_, m) => _logger.LogInformation("[Kafka Log] {Facility}: {Message}", m.Facility, m.Message))
        .Build();
       // _logger.LogInformation("[VoyageConsumer] BootstrapServers = {Bs}", config.BootstrapServers);
        consumer.Subscribe(SagaTopics);
        _logger.LogInformation("[VoyageConsumer] Subscribed to {Count} topics", SagaTopics.Length);
        while (!stoppingToken.IsCancellationRequested) 
        {
            try
            {
                _logger.LogInformation("[VoyageConsumer] Waiting for messages...");
                var result = consumer.Consume(stoppingToken);
                if (result is null) continue;

                _logger.LogInformation("[VoyageConsumer] Received {Topic} key={Key}", result.Topic, result.Message.Key);
                using var scope = _scopeFactory.CreateScope();
                var voyageRepository = scope.ServiceProvider.GetRequiredService<IVoyageRepository>();
                switch (result.Topic)
                {

                    case KafkaTopics.VoyageRequested:
                        _logger.LogInformation("[VoyageConsumer] Processing VoyageRequested message: {Message}", result.Message.Value);
                        // await voyageRepository.UpdateAsync(JsonSerializer.Deserialize<Domain.Entities.AggregateRoots.Voyage>(result.Message.Value), stoppingToken);
                        break;
                    default:
                        _logger.LogWarning("[VoyageConsumer] No handler for topic {Topic}", result.Topic);
                        break;


                }
                consumer.Commit(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[VoyageConsumer] Error processing message — will retry");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            
        
        }


    }
}

