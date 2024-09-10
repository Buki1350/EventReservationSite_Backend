using DotNetBoilerplate.Core.CommonExceptions;

namespace DotNetBoilerplate.Core.Event;

public sealed record EventLocation
{
    public EventLocation(string value)
    {
        if (value is null) throw new InvalidEntityStringException();
        
        Value = value;
    }

    public EventLocation()
    {
    }
    
    public string Value { get; }
    
    public static implicit operator EventLocation(string value) => new EventLocation(value);

    public static implicit operator string(EventLocation value) => value.Value;
}