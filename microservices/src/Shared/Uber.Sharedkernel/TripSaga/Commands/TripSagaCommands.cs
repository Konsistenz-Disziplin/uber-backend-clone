using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Sharedkernel.TripSaga.Commands;

public sealed record FindDriverCommand(
    Guid TripId,
    string PickupLocation,
    decimal EstimatedFare);

public sealed record LockDriverCommand(
    Guid TripId,
    Guid DriverId);

public sealed record ReleaseDriverCommand(
    Guid TripId,
    Guid DriverId);

public sealed record NotifyPassengerCommand(
    Guid TripId,
    Guid PassengerId,
    string Message);

public sealed record OpenBillingSessionCommand(
    Guid TripId,
    Guid PassengerId,
    Guid DriverId,
    decimal EstimatedFare);

public sealed record ProcessPaymentCommand(
    Guid TripId,
    Guid PassengerId,
    decimal Amount);

public sealed record CancelTripCommand(
    Guid TripId,
    string Reason);