using DotNetBoilerplate.Core.CommonExceptions;

namespace DotNetBoilerplate.Core.Event;

public sealed record EventTitle
{
    public EventTitle(string value)
    {
        if (value is null) throw new InvalidEntityStringException();
        
        Value = value;
    }
    
    public EventTitle(){}
    
    public string Value { get; }
    
    public static implicit operator EventTitle(string value) => new EventTitle(value);
    
    public static implicit operator string(EventTitle value) => value.Value;
}