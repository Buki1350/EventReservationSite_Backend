using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Events;

public class InvalidEventLocationException() : CustomException("The event location is null or empty.") { }
public sealed record EventLocation
{
    public EventLocation(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new InvalidEventLocationException();

        Value = value;
    }
    
    public string Value { get; }
    public static implicit operator string(EventLocation value) => value.Value;
    public static implicit operator EventLocation(string value) => new EventLocation(value);
}