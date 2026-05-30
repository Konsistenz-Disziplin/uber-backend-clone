using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Uber.Shared.Primitives;
using Uber.Voyage.Domain.Enums;
using Uber.Voyage.Domain.Events;
using Uber.Voyage.Domain.ValueObjects;

namespace Uber.Voyage.Domain.Entities;

public sealed class Voyage : AggregateRoot
{
    public Guid RiderId { get; private set; }
    public Guid? DriverId { get; private set; }
    public Location Pickup { get; private set; } = default!;
    public Location Dropoff { get; private set; } = default!;
    //public Fare Fare { get; private set; } = default!;
    public VoyageStatus Status { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }
    public static Voyage Create(
        Guid riderId, Coordinates pickup,
    Coordinates dropoff,VehicleType vehicleType)
    {
        var voyage = new Voyage
        {
            Id = Guid.NewGuid(),
            RiderId = riderId,
            Pickup = Location.Create(pickup),
            Dropoff = Location.Create(dropoff),
            VehicleType = vehicleType,
            Status = VoyageStatus.Requested,
            CreatedAtUtc = DateTime.UtcNow
        };
        voyage.RaiseDomainEvent(new VoyageRequestedEvent(voyage.Id, riderId));
        return voyage;
    }

}
