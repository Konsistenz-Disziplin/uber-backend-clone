using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;
using Uber.Voyage.Domain.ValueObjects;

namespace Uber.Voyage.Domain.Entities;

public sealed class Location : ValueObject
{
    public Location() { }
    public Coordinates Coordinates { get; private set; } = default!;
    public string? Address { get; private set; }

    public static Location Create(Coordinates coordinates, string? address = null) =>
      new() {Coordinates = coordinates, Address = address };

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Coordinates;
        if (Address is not null) yield return Address;
    }
}

