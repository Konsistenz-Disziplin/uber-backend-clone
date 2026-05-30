using Uber.Shared.Primitives;
using Uber.Voyage.Domain.Errors;

namespace Uber.Voyage.Domain.ValueObjects;

public sealed class Coordinates : ValueObject
{
    private Coordinates() { }

    public double Latitude { get; private init; }
    public double Longitude { get; private init; }

    public static Result<Coordinates> Create(double lat, double lng)
    {
        if (lat is < -90 or > 90) return VoyageErrors.InvalidLatitude;
        if (lng is < -180 or > 180) return VoyageErrors.InvalidLongitude;
        return new Coordinates { Latitude = lat, Longitude = lng };
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Latitude;
        yield return Longitude;
    }
}
