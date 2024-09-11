namespace DotNetBoilerplate.Core.Events;

public sealed record EventDescription
{
    public EventDescription(string value)
    {
        Value = value;
    }
    
    public EventDescription(){}
    public string Value { get; }
    public static implicit operator string(EventDescription value) => value.Value;
    public static implicit operator EventDescription(string value) => new EventDescription(value);
    
    
}