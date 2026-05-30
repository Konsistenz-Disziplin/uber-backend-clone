using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Voyage.Application.Features.RequestTrip;

internal sealed class RequestVoyageCommandValidator : AbstractValidator<RequestVoyageCommand>
{
    public RequestVoyageCommandValidator()
    {
        RuleFor(x => x.PassengerId).NotEmpty();
        RuleFor(x => x.PickupLat).InclusiveBetween(-90, 90);
        RuleFor(x => x.PickupLng).InclusiveBetween(-180, 180);
        RuleFor(x => x.DropoffLat).InclusiveBetween(-90, 90);
        RuleFor(x => x.DropoffLng).InclusiveBetween(-180, 180);
        RuleFor(x => x.VehicleType).IsInEnum();
        RuleFor(x => x).Must(c =>
          !(c.PickupLat == c.DropoffLat && c.PickupLng == c.DropoffLng))
          .WithMessage("Pickup and dropoff cannot be identical.");
    }
}
