using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Shared.VoyageSaga.Commands;

public sealed record FindDriverCommand(
    Guid TripId,
    string PickupLocation,
    decimal EstimatedFare);
