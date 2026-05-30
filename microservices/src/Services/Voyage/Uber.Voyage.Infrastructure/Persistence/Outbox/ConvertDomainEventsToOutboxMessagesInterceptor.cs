using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Uber.Shared.Primitives;

namespace Uber.Voyage.Infrastructure.Persistence.Outbox;

public class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        
        var aggregates = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var outboxMessages = aggregates
            .SelectMany(a => a.DomainEvents)
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredAt = DateTime.UtcNow,
                Type = domainEvent.GetType().AssemblyQualifiedName!,
                Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType())
            })
            .ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

        foreach (var aggregate in aggregates)
            aggregate.ClearDomainEvents();

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
