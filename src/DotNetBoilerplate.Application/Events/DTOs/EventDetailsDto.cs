using DotNetBoilerplate.Application.Reservations.DTOs;
using DotNetBoilerplate.Core.Events;

namespace DotNetBoilerplate.Application.Events.DTOs;

public record EventDetailsDto(Guid Id, string Title, List<ReservationDto> Reservations)
{
    public EventDetailsDto(Event @event) : this(@event.Id, @event.Title, @event.Reservations.Select(x => new ReservationDto(x)).ToList()) { }
};