using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Shared.Primitives;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
