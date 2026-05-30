using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;

namespace Uber.Voyage.Domain.Events;

public sealed record VoyageRequestedEvent(Guid TripId, Guid RiderId) : DomainEvent;
