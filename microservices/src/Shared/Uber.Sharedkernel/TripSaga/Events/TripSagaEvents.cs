using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Sharedkernel.TripSaga.Events;

public sealed record TripRequestedEvent(
    Guid TripId,
    Guid PassengerId,
    string PickupLocation,
    string DropoffLocation,
    decimal EstimatedFare,
    DateTime OccurredAt);

public sealed record DriverFoundEvent(
    Guid TripId,
    Guid DriverId,
    DateTime OccurredAt);

public sealed record NoDriverFoundEvent(
    Guid TripId,
    DateTime OccurredAt);

public sealed record DriverAcceptedEvent(
    Guid TripId,
    Guid DriverId,
    DateTime OccurredAt);

public sealed record DriverCancelledEvent(
    Guid TripId,
    Guid DriverId,
    string Reason,
    DateTime OccurredAt);

public sealed record TripStartedEvent(
    Guid TripId,
    Guid DriverId,
    DateTime OccurredAt);

public sealed record TripCompletedEvent(
    Guid TripId,
    Guid DriverId,
    decimal FinalFare,
    DateTime OccurredAt);

public sealed record PaymentProcessedEvent(
    Guid TripId,
    Guid PassengerId,
    decimal Amount,
    DateTime OccurredAt);

public sealed record PaymentFailedEvent(
    Guid TripId,
    Guid PassengerId,
    string Reason,
    DateTime OccurredAt);