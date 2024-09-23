using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Events;

public class InvalidEventTitleException() : CustomException("The event title cannot be empty.");

public sealed record EventTitle
{
    public EventTitle(){}
    public EventTitle(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new InvalidEventTitleException();
        
        Value = value;
    }
    
    public string Value { get; init; }
    public static implicit operator string(EventTitle value) => value.Value;
    public static implicit operator EventTitle(string value) => new EventTitle(value);
}

