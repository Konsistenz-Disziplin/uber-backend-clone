using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Shared.Primitives;

public abstract record DomainEvent(Guid Id, DateTime OccurredOnUtc) : IDomainEvent
{
    protected DomainEvent() : this(Guid.NewGuid(), DateTime.UtcNow) { }
}