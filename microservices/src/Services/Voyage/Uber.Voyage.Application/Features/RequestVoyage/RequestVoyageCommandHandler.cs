using System;
using System.Collections.Generic;
using System.Text;
using Uber.Voyage.Application.Abstractions;
using Uber.Voyage.Domain.Repositories;
using Uber.Voyage.Domain.Entities;
using Uber.Voyage.Domain.ValueObjects;
using Uber.Shared.Primitives;
namespace Uber.Voyage.Application.Features.RequestTrip
{
    internal sealed class RequestVoyageCommandHandler(
  IVoyageRepository voyageRepository,
  IUnitOfWork uow)
    {
        public async Task<Result<Guid>> Handle(RequestVoyageCommand command, CancellationToken ct)
        {
            var pickup = Coordinates.Create(command.PickupLat, command.PickupLng);
            var dropoff = Coordinates.Create(command.DropoffLat, command.DropoffLng);
            if (pickup.IsFailure) return pickup.Error;
            if (dropoff.IsFailure) return dropoff.Error;
            //var fare = Fare.Create
            var voyage = Domain.Entities.Voyage.Create(
            command.PassengerId,
            pickup.Value,
            dropoff.Value,
            command.VehicleType);

            await voyageRepository.AddAsync(voyage, ct);
            await uow.SaveChangesAsync(ct);
            return voyage.Id;
        }
    }
}
