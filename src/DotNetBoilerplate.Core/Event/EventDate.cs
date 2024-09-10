namespace DotNetBoilerplate.Core.Event;

public sealed record EventDate
{
    public EventDate(DateTime value)
    {
        Value = value;
    }
    
    public DateTime Value { get; init; }
}