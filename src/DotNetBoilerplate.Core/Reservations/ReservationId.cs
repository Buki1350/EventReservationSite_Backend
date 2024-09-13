using DotNetBoilerplate.Core.CommonExceptions;

namespace DotNetBoilerplate.Core.Reservations;

public sealed record ReservationId
{
    public ReservationId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }
    
    public Guid Value { get; }
    public static implicit operator Guid(ReservationId value) => value.Value;
    public static implicit operator ReservationId(Guid value) => new ReservationId(value);
    
}
