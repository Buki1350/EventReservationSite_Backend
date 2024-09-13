namespace DotNetBoilerplate.Core.Events;

public sealed record EventEndDate
{
    public EventEndDate(DateTime value)
    {
        Value = value;
    }
    
    public DateTime Value { get; private set; }
    public static implicit operator DateTime(EventEndDate eventEndDate) => eventEndDate.Value;
    public static implicit operator EventEndDate(DateTime value) => new EventEndDate(value);
}