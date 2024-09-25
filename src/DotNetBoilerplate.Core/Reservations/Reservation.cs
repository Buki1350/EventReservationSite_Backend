using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Core.Users;
using UserId = DotNetBoilerplate.Core.Users.UserId;

namespace DotNetBoilerplate.Core.Reservations;

public sealed class Reservation
{
    public Reservation(){}

    public static Reservation Create(
        EventId eventId,
        UserId userId,
        DateTime now
    )
    {
        var reservation = new Reservation()
        {
            Id = Guid.NewGuid(),
            CreatedAt = now,
            UserId = userId,
            EventId = eventId,
            Active = false
        };
        
        return reservation;
    }

    public void SetPaymentStatus(bool value) { Paid = value; }
    
    public bool IsActive() { return Active; }
    public void Cancel() { Active = false; }
    public bool IsPaid() { return Paid.Value; }
    
    public ReservationId Id { get; private set; }
    public UserId UserId { get; private set; }
    public EventId EventId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ReservationPaid Paid { get; private set; } = false;
    public bool Active { get; private set; }
    
    
}