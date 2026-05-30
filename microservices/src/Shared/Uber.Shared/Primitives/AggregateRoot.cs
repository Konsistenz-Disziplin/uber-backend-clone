using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;
namespace Uber.Shared.Primitives;

public abstract class AggregateRoot : Entity<Guid>
{
    protected AggregateRoot(Guid id) : base(id) { }
    protected AggregateRoot() { }
}