using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Errors;

namespace Uber.Voyage.Domain.Errors;

public static class VoyageErrors
{
    public static readonly Error NotFound =
  new("Voyage.NotFound", "Voyage not found." , ErrorType.NotFound);
    public static readonly Error InvalidStatusTransition =
      new("Voyage.InvalidStatusTransition", "Status transition not allowed.");
    public static readonly Error InvalidFareAmount =
      new("Voyage.InvalidFareAmount", "Fare amount must be greater than zero.");
    public static readonly Error InvalidSurgeMultiplier =
      new("Voyage.InvalidSurgeMultiplier", "Surge multiplier must be 1.0–4.0.");
    public static readonly Error InvalidLatitude =
      new("Voyage.InvalidLatitude", "Latitude must be -90 to 90.");
    public static readonly Error InvalidLongitude =
      new("Voyage.InvalidLongitude", "Longitude must be -180 to 180.");
}
