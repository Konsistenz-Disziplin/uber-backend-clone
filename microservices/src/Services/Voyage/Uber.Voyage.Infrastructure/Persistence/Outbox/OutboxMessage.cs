using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Voyage.Infrastructure.Persistence.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Payload { get; init; } = string.Empty;
    public DateTime OccurredAt { get; init; }
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }
}
