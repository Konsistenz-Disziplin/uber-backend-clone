using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Shared.Primitives;

public abstract class Entity<TId>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity()
    {

    }
    protected Entity(TId id) => Id = id;

    public TId Id { get; init; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
