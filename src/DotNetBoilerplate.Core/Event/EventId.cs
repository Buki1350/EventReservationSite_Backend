namespace DotNetBoilerplate.Core.Event;

public sealed record EventId
{
    public EventId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; init; }
}