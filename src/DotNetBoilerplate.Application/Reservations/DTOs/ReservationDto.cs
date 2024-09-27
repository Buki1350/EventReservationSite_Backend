using DotNetBoilerplate.Core.Reservations;

namespace DotNetBoilerplate.Application.Reservations.DTOs;

public record ReservationDto(Guid Id, DateTime CreatedAt)
{
    public ReservationDto(Reservation reservation) : this(reservation.Id, reservation.CreatedAt){ }
};