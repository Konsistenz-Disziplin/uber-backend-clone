using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;
using Uber.Voyage.Domain.Errors;

namespace Uber.Voyage.Domain.Entities.ValueObjects;

public sealed class Fare : ValueObject
{
    private Fare() { }

    public decimal Amount { get; private init; }
    public string Currency { get; private init; } = "USD";
    public double SurgeMultiplier { get; private init; }
    public double DistanceKm { get; private init; }
    public int EtaMinutes { get; private init; }

    public static Result<Fare> Create(
      decimal amount, string currency, double surge,
      double distanceKm, int etaMinutes)
    {
        if (amount <= 0) return VoyageErrors.InvalidFareAmount;
        if (surge is < 1.0 or > 4.0) return VoyageErrors.InvalidSurgeMultiplier;

        return new Fare
        {
            Amount = amount,
            Currency = currency,
            SurgeMultiplier = surge,
            DistanceKm = distanceKm,
            EtaMinutes = etaMinutes
        };
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
        yield return SurgeMultiplier;
    }
}