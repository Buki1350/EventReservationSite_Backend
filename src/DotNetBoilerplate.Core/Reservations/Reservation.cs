using DotNetBoilerplate.Core.Users;

namespace DotNetBoilerplate.Core.Reservations;

public sealed class Reservation
{
    public Reservation(){}

    public static Reservation Create(
        UserId userId,
        EventId eventId,
        ReservationPaid isPaid
    )
    {
        var reservation = new Reservation()
        {
            UserId = userId,
            EventId = eventId,
            IsPaid = isPaid
        };
        
        return reservation;
    }
    
    public UserId UserId { get; private set; }
    public EventId EventId { get; private set; }
    public ReservationPaid IsPaid { get; private set; }
    
    
}