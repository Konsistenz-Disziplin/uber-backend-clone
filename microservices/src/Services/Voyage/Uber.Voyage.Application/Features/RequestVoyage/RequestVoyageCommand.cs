using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;
using Uber.Voyage.Domain.Enums;

namespace Uber.Voyage.Application.Features.RequestTrip;

public sealed record RequestVoyageCommand
(
      Guid PassengerId,
      double PickupLat,
      double PickupLng,
      string? PickupAddress,
      double DropoffLat,
      double DropoffLng,
      string? DropoffAddress,
      VehicleType VehicleType
)
: IRequest<Result<Guid>>;
