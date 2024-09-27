namespace DotNetBoilerplate.Core.Reservations;

public sealed record ReservationPaid
{
    public ReservationPaid(bool value)
    {
        Value = value;
    }

    public bool Value { get; }
    public static implicit operator bool(ReservationPaid value) => value.Value;
    public static implicit operator ReservationPaid(bool value) => new ReservationPaid(value);
    
}