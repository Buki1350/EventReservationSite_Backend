using DotNetBoilerplate.Core.Event;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Core.Reservation;

public class Reservation : Entity
{
    private Reservation(ReservationId id, EventId eventId, UserId userId)
    {
        Id = id;
        EventId = eventId;
        UserId = userId;
    }

    public ReservationId Id { get; private set; }
    public EventId EventId { get; set; }
    public UserId UserId { get; set; }
}