using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Shared.VoyageSaga.Events;

public sealed record DriverAcceptedEvent(
    Guid TripId,
    Guid DriverId,
    DateTime OccurredAt);