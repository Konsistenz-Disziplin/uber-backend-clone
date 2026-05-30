using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Uber.Shared;
using Uber.Voyage.Application.Abstractions;
using Uber.Voyage.Infrastructure.Kafka;

namespace Uber.Voyage.Infrastructure.Persistence.Outbox;

public sealed class OutboxProcessor : BackgroundService
{

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OutboxProcessor> _logger;

    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(5);

    public OutboxProcessor(IServiceScopeFactory scopeFactory, ILogger<OutboxProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[OutboxProcessor] Unhandled error");
            }

            await Task.Delay(Interval, stoppingToken);
        }
    }

    private async Task ProcessAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<VoyageDbContext>();
        var producer = scope.ServiceProvider.GetRequiredService<IKafkaProducer>();

        var messages = await dbContext.Set<OutboxMessage>()
            .Where(m => m.ProcessedAt == null)
            .OrderBy(m => m.OccurredAt)
            .Take(50)
            .ToListAsync(ct);

        if (!messages.Any()) return;

        foreach (var message in messages)
        {
            try
            {
                var eventType = Type.GetType(message.Type);

                if (eventType is null)
                {
                    _logger.LogWarning(
                        "[OutboxProcessor] Cannot resolve type {Type} for message {Id}",
                        message.Type, message.Id);

                    message.ProcessedAt = DateTime.UtcNow;
                    message.Error = $"Type not found: {message.Type}";
                    continue;
                }

                var topic = ResolveTopicFromType(eventType);

                var domainEvent = JsonSerializer.Deserialize(message.Payload, eventType)!;

                await producer.PublishAsync(
                    topic,
                    message.Id.ToString(),
                    domainEvent,
                    ct);

                message.ProcessedAt = DateTime.UtcNow;

                _logger.LogInformation(
                    "[OutboxProcessor] Published {Type} → {Topic}",
                    eventType.Name, topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "[OutboxProcessor] Failed to publish message {Id} — will retry",
                    message.Id);

                message.Error = ex.Message;
            }
        }

        await dbContext.SaveChangesAsync(ct);
    }

    private static string ResolveTopicFromType(Type type) => type.Name switch
    {
        "TripRequestedDomainEvent" => KafkaTopics.TripRequested,
        "TripAcceptedDomainEvent" => KafkaTopics.DriverAccepted,
        "TripStartedDomainEvent" => KafkaTopics.TripStarted,
        "TripCompletedDomainEvent" => KafkaTopics.TripCompleted,
        "TripCancelledDomainEvent" => KafkaTopics.TripCancelled,
        _ => throw new InvalidOperationException($"No topic mapped for: {type.Name}")
    };
}