namespace DotNetBoilerplate.Core.Reservation;

public sealed class ReservationId
{
    public ReservationId(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}