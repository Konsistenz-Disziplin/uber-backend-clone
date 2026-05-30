namespace Uber.Voyage.Application.Features.GetVoyage;

public sealed record GetVoyageByIdResponse(
    Guid Id,
    Guid RiderId,
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