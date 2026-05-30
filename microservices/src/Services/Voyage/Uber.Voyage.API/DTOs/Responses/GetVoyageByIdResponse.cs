namespace Uber.Voyage.API.DTOs.Responses;

public sealed record GetVoyageByIdResponse(
    Guid Id,
    Guid PassengerId,
    Guid? DriverId,
    string Status,
    string PickupLocation,
    string DropoffLocation,
    decimal EstimatedFare,
    decimal? FinalFare,
    DateTime RequestedAt,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    DateTime? CancelledAt,
    string? CancellationReason);