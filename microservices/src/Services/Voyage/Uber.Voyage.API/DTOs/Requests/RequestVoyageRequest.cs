using Uber.Voyage.Domain.Enums;

namespace Uber.Voyage.API.DTOs.Requests;

public record RequestVoyageRequest(
Guid PassengerId,
double PickupLat,
double PickupLng,
string? PickupAddress,
double DropoffLat,
double DropoffLng,
string? DropoffAddress,
VehicleType VehicleType);
