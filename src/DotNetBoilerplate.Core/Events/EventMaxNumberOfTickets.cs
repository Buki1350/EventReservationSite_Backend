using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Events;

public class InvalidMaxNumberOfTicketsException(int value)
    : CustomException($"Incorrect maximum number of tickets: {value}");

public sealed record EventMaxNumberOfTickets
{
    public EventMaxNumberOfTickets(int value)
    {
        if (value <= 0) throw new InvalidMaxNumberOfTicketsException(value);

        Value = value;
    }
    
    public int Value { get; }
    public static implicit operator int(EventMaxNumberOfTickets value) => value.Value;
    public static implicit operator EventMaxNumberOfTickets(int value) => new(value);
}